import logging
import scrapy.http
from ZomSpider.Utils import PATH
from scrapy_selenium import SeleniumRequest
from ZomSpider.Item.Restaurant import Restaurant
from scrapy.http.response.html import HtmlResponse
from ZomSpider.settings import ZOMATO_MELB_COLLECTION
from ZomSpider.SpecificSpider.RestaurantParser import RestaurantParser
from ZomSpider.SpecificSpider.CollectionParser import CollectionSpider


class FoodSpider(scrapy.Spider):
    name = "restaurant"
    allowed_domains = ['www.zomato.com']

    def __init__(self, **kwargs):
        super().__init__(name=None, **kwargs)

    def start_requests(self):
        self.logger.setLevel(logging.INFO)
        # parsing collections in mel area
        collection_urls = CollectionSpider(ZOMATO_MELB_COLLECTION).scrap_collection_url()

        for url in collection_urls:
            self.logger.info("url scrapped {0}".format(url))
            yield SeleniumRequest(url=url, callback=self.parse, wait_time=1)

    # parsing cards of collections
    def parse(self, response: HtmlResponse):

        logging.getLogger("urllib3").setLevel(logging.INFO)
        # driver = response.request.meta['driver']
        self.logger.info("url in parse {0}".format(response.url))

        collection_name = response.url.split("/")[-1].replace("-", " ")
        res_in_collection = response.selector.xpath(PATH.RES_IN_COLLECTION)

        for restaurant_card in res_in_collection:

            res_name = restaurant_card.xpath('.' + PATH.RES_CARD).attrib['title']
            res_link = restaurant_card.xpath('./a').attrib['href']

            hashed_id = self.get_hashed_id(res_link)

            res_rating = restaurant_card.xpath('.' + PATH.RES_RATING + '/text()').get().strip()
            res_zone = restaurant_card.xpath('.' + PATH.RES_ZONE + '/div/text()').get().split(",")

            restaurant = Restaurant()
            # default field
            restaurant['liked'] = False
            restaurant['been_there_count'] = 0

            # filling fields
            restaurant['id'] = hashed_id
            restaurant['rating'] = 0.0 if res_rating == 'NEW' else float(res_rating)
            restaurant['name'] = res_name.strip()
            restaurant['zone'] = res_zone
            restaurant['link'] = res_link
            restaurant['collection'] = [collection_name]

            # res_link = 'https://www.zomato.com/melbourne/trinket-1-cbd?zrp_bid=357104&zrp_pid=14&zrp_cid=390614'
            yield SeleniumRequest(url=res_link,
                                  callback=self.parse_restaurant_page,
                                  meta={'item': restaurant},
                                  wait_time=1)

    def parse_restaurant_page(self, response: HtmlResponse):
        restaurant = response.meta['item']
        parser = RestaurantParser(restaurant['name'], response.url, response.selector)

        try:
            # extract fields
            restaurant['restaurant_type'] = parser.get_res_type()
            restaurant['phone_number'] = parser.get_res_phone_num()

            # res info include cuisine_type
            restaurant['cuisine_type'] = parser.get_res_cuisine_types()

            # parsing addresss
            restaurant['address'] = parser.get_res_address()
            # opening hours and avg_cost [0] is avg cost, [1] is opening hours
            # elements = driver.find_elements_by_xpath('//div[contains(@class,"res-info-detail")]')
            restaurant['avg_cost'] = parser.get_rest_avg_cost()

            restaurant['bill_acceptance'] = parser.get_res_bill_acceptance()

            restaurant['opening_hours'] = parser.get_res_opening_hours()

            restaurant['menu_img_links'] = parser.get_res_menu()

            # parse 10 food images
            restaurant['food_image_links'] = parser.get_food_img()

            book = parser.get_food_img_juced()
            # restaurant['menu_img_links'] = book['menu']
            # restaurant['food_image_links'] = book['food_img']

            # self.dividePrint(restaurant)
            self.logger.info("restaurant parsed {0}".format(restaurant))

            yield restaurant

        except Exception as e:
            self.logger.error("in parse_restaurant_page: {0} ".format(e))

    def dividePrint(self, content):

        print("*" * 147)
        print(content)
        print("*" * 147)

    def get_hashed_id(self, res_link):
        import hashlib
        return int(hashlib.sha256(res_link.encode('utf-8')).hexdigest(), 16) % (10 ** 10)
