from ZomSpider.Utils import PATH
from ZomSpider.Utils.SeleniumDriver import SeleniumDriver


class CollectionSpider(SeleniumDriver):

    def __init__(self, url):
        super().__init__()
        self.url = url
        self.links = list()

    def scrap_collection_url(self):
        # start crawling
        self.logger.debug("Scraping start")
        self.driver.get(self.url)
        self.driver.implicitly_wait(1)

        # return ['https://www.zomato.com/melbourne/top-restaurants']
        try:
            pages = self.driver.find_elements_by_xpath(PATH.COLLECTION_CARD)
            self.logger.info(str(len(pages)) + "links added")

            for page in pages:
                val = page.get_attribute("href")
                self.links.append(val)

            return self.links

        except Exception as e:
            self.logger.error("exceptions in CollectionParser: {0}".format(e))

        finally:
            self.driver.close()
            self.driver.quit()
