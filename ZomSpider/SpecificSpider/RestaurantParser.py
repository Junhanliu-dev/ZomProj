import re
import time
import logging
import multiprocessing
from typing import List, Dict
from scrapy import Selector
from ZomSpider.Utils import PATH
from selenium.webdriver.common.by import By
from selenium.webdriver.support.wait import WebDriverWait
from ZomSpider.Utils.SeleniumDriver import SeleniumDriver
from selenium.webdriver.remote.webelement import WebElement
from selenium.common.exceptions import NoSuchElementException
from selenium.webdriver.support import expected_conditions as ec


class RestaurantParser:

    def __init__(self, name, url, selector: Selector):
        self.name = name
        self.url = url
        self.selector = selector
        self.logger = logging.getLogger(self.name)
        self.logger.setLevel(logging.INFO)
        self.load_resources()

    def load_resources(self):
        consoleHandler = logging.StreamHandler()
        consoleHandler.setLevel(logging.INFO)
        consoleHandler.setFormatter(logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s'))
        self.logger.addHandler(consoleHandler)
        self.logger.info("Restaurant parser for {0} is ready".format(self.name))

    def get_res_type(self) -> List:

        return self.selector.xpath(PATH.RES_INFO + '//a/text()').getall()

    def get_res_phone_num(self) -> str:

        return self.selector.xpath('//span[@class="tel"]/text()').get()

    def get_res_cuisine_types(self) -> List:

        return self.selector.xpath('//div[contains(@class,"res-info-cuisines")]//a/text()').getall()

    def get_res_address(self) -> str:
        address = self.selector.xpath('//div[contains(@class,"res-main-address")]//span/text()').getall()

        return ''.join(address)

    def get_rest_avg_cost(self) -> int:
        avg_cost_container = self.selector.xpath('.//div[contains(@class,"res-info-detail")]')[0]
        price_tag = avg_cost_container.xpath('.//span/@aria-label').get().strip()

        pattern = r"(?<=\$)\d+(?:\.\d+)?"
        price_tag = int(re.findall(pattern, price_tag)[0])

        return price_tag

    def get_res_bill_acceptance(self) -> List:
        container = self.selector.xpath('.//div[contains(@class,"res-info-detail")]')[0]
        payment_xpath = './/div[contains(@class,"res-info-cft-text")]//span[@itemprop="paymentAccepted"]'

        acceptance = container.xpath(payment_xpath + '/text()').getall()

        return acceptance

    def get_res_opening_hours(self) -> dict:
        container = self.selector.xpath('.//div[contains(@class,"res-info-detail")]')[1]
        time_table_tr = container.xpath('.//div[@id = "res-week-timetable"]/table//tr')
        table = dict()

        for day in time_table_tr:
            date = day.xpath('.//td/text()').getall()

            # date monday..tuesday ...
            r = date[0]
            opening_hour = date[1]

            table[r] = opening_hour

        return table

    def get_res_menu(self, return_dict=None):
        driver = SeleniumDriver().get_driver()
        driver.get(self.url)
        driver.implicitly_wait(1)
        time.sleep(1)
        menu_dic = {}

        try:
            # click on the menu tab
            driver.find_element_by_xpath('//a[contains(@data-tab_type,"menu")]').click()
            driver.implicitly_wait(1)
            time.sleep(1)

            # get num of pages
            page_num_pattern = r"(?<=of )\d.*$"
            total_pages = int(re.findall(page_num_pattern, driver
                                         .find_element_by_xpath('//div[contains(@class,"pagination-meta")]/div')
                                         .text)[0])
        except NoSuchElementException as e:
            self.logger.warning("No clickable menu tab on url: {0}\n msg: {1}".format(self.url, e))
            self.close_driver(driver)
            if return_dict is not None:
                return_dict['menu'] = menu_dic
                return
            else:
                return menu_dic
        except Exception as e:
            self.logger.error("error occurs: {0}".format(e))
            self.close_driver(driver)
            if return_dict is not None:
                return_dict['menu'] = menu_dic
                return
            else:
                return menu_dic

        if total_pages == 0:
            self.logger.info("no menu available on url: {0}".format(self.url))

            if return_dict is not None:
                return_dict['menu'] = menu_dic
                return
            else:

                return menu_dic

        menu_category = [i.text for i in driver.find_elements_by_xpath(PATH.RES_SUB_TAB)]
        if len(menu_category) == 0:
            menu_dic['menu'] = []
        else:
            menu_dic = {i: [] for i in menu_category}

        try:
            for _ in range(total_pages):
                current_tab = 'menu' if len(menu_category) == 0 else driver.find_element_by_xpath(PATH.ACTIVE_TAB).text

                menu_url = driver.find_element_by_id('menu-image') \
                    .find_element_by_tag_name('img') \
                    .get_attribute('src')

                pattern = r'\?.*'
                original_url = re.sub(pattern, '', menu_url)
                menu_dic[current_tab].append(original_url)

                # to next menu link
                driver.find_element_by_id('menu-next-page').click()
                driver.implicitly_wait(0.5)
                time.sleep(0.5)

        except Exception as e:
            self.logger.error("error at get_res_menu on page {0} \n"
                              "error : {1}".format(driver.current_url, e))

        finally:
            self.close_driver(driver)
            self.logger.info('driver on get_res_menu closed. url: {0}'.format(self.url))

            if return_dict is not None:
                return_dict['menu'] = menu_dic
                return
            else:
                return menu_dic

    def get_food_img(self, return_dict=None):
        driver = SeleniumDriver().get_driver()
        driver.get(self.url)
        driver.implicitly_wait(1)
        time.sleep(1)
        photo_links = []

        def extract_links(boxes: List[WebElement]):
            links = []
            pattern = r'\?.*'
            for box in boxes:
                box_url = re.sub(pattern, '', box.find_element_by_tag_name('img').get_attribute('src'))
                links.append(box_url)

            return links

        try:
            photo_tab = driver.find_element_by_xpath('//a[contains(@class,"photosTab")]')
            photo_count = int(photo_tab.get_attribute('data-count'))

        except NoSuchElementException as e:
            self.logger.info("No photos tab on res {0}, url: {1} \n error: {2}".format(self.name, self.url, e))
            photo_tab = None
            if return_dict is not None:
                return_dict['food_img'] = photo_links
                return
            else:
                return photo_links

        except Exception as e:
            self.logger.error("Unknown error: {0}".format(e))
            if return_dict is not None:
                return_dict['food_img'] = photo_links
                return
            else:
                return photo_links

        if photo_count > 0:
            # click on photos tab
            if photo_tab is not None:
                photo_tab.click()

            wait = WebDriverWait(driver, 3)
            wait.until(ec.visibility_of_element_located((By.XPATH, '//div[contains(@id,"photo_page")]')))

            try:
                food_tab = driver.find_element_by_xpath('//a[contains(@data-category,"food")]')

            except NoSuchElementException as e:
                self.logger.warn("No food tab on res {0},"
                                 "\n url: {1}. "
                                 "\nerror: {2}"
                                 .format(self.name, self.url, e.stacktrace))
                food_tab = None
            except Exception as e:
                self.logger.warning("Unknown Error while parsing res: {0} \n url: {1} \nerror : {2}"
                                    .format(self.name, self.url, e))
                food_tab = None

            if food_tab is not None:
                food_tab.click()
                wait.until(ec.visibility_of_element_located((By.XPATH, '//div[contains(@id,"photo_page")]')))

            photo_boxes = driver.find_elements_by_xpath('//div[contains(@class,"photobox")]')

            if len(photo_boxes) <= 10:
                photo_links = extract_links(photo_boxes)

                if return_dict is not None:
                    return_dict['food_img'] = photo_links
                    return
                else:
                    return photo_links

            else:
                load_more_tab = driver.find_elements_by_xpath('//div[contains(@class,"picLoadMore")]')
                counter = 0

                while len(load_more_tab) > 0 and counter < 5:
                    load_more_tab[0].click()
                    driver.implicitly_wait(0.5)
                    time.sleep(0.5)
                    try:
                        load_more_tab = driver.find_elements_by_xpath('//div[contains(@class,"picLoadMore")]')
                    except Exception as e:
                        self.logger.warning(e)
                        break
                    counter += 1

                print("loading pic ended")

            import random
            photo_boxes = driver.find_elements_by_xpath('//div[contains(@class,"photobox")]')
            picked_boxes = random.sample(photo_boxes, 10)

            photo_links = extract_links(picked_boxes)

        self.close_driver(driver)

        if return_dict is not None:
            return_dict['food_img'] = photo_links
            return
        else:
            return photo_links

    # TODO
    def get_food_img_juced(self) -> Dict:
        manager = multiprocessing.Manager()
        return_dict = manager.dict()
        jobs = []
        menu_links_worker = multiprocessing.Process(target=self.get_res_menu, args=(return_dict,))
        food_image_links_worker = multiprocessing.Process(target=self.get_food_img, args=(return_dict,))

        jobs.append(menu_links_worker)
        jobs.append(food_image_links_worker)

        for job in jobs:
            job.start()
        for job in jobs:
            job.join()

        return return_dict

    def close_driver(self, driver):
        driver.close()
        driver.quit()
        self.logger.info("driver closed. url:{0}".format(self.url))

