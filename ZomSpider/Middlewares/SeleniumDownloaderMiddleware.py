import time
from scrapy import signals
from scrapy.http import HtmlResponse
from scrapy.utils.python import to_bytes
from selenium import webdriver
from selenium.webdriver.chrome.options import Options

from ZomSpider.settings import USER_AGENT


class SeleniumDownloaderMiddleware:

    def __init__(self):
        self.driver = None

    @classmethod
    def from_crawler(cls, crawler):
        middleware = cls()
        crawler.signals.connect(middleware.spider_opened, signals.spider_opened)
        crawler.signals.connect(middleware.spider_closed, signals.spider_closed)
        return middleware

    def process_request(self, request, spider):
        self.driver.get(request.url)
        self.driver.implicitly_wait(0.5)
        time.sleep(0.5)
        body = to_bytes(self.driver.page_source)
        return HtmlResponse(self.driver.current_url, body=body, encoding='utf-8', request=request)

    def spider_opened(self):
        options = Options()
        options.add_argument("--headless")
        options.add_argument('user-agent={0}'.format(USER_AGENT))
        self.driver = webdriver.Chrome(executable_path='/usr/local/bin/chromedriver', options=options)

    def spider_closed(self, spider):

        if self.driver:
            self.driver.close()
            self.driver.quit()
            self.driver = None
