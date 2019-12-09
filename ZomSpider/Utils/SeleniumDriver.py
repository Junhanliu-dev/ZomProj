from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from ZomSpider.settings import USER_AGENT
from selenium.webdriver.remote.remote_connection import LOGGER
import logging


class SeleniumDriver:

    def __init__(self):
        # setup logger
        self.logger = logging.getLogger("SeleniumDriver")
        self.logger.setLevel(logging.INFO)
        # setup display
        LOGGER.setLevel(logging.INFO)

        logging.getLogger("urllib3").setLevel(logging.INFO)
        # setup driver
        chrome_options = Options()
        chrome_options.add_argument("--headless")
        chrome_options.add_argument('--no-sandbox')
        chrome_options.add_argument('user-agent={0}'.format(USER_AGENT))
        self.driver = webdriver.Chrome(executable_path='/usr/local/bin/chromedriver', options=chrome_options)

        # logger setting
        consoleHandler = logging.StreamHandler()
        consoleHandler.setLevel(logging.INFO)
        consoleHandler.setFormatter(logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s'))
        self.logger.addHandler(consoleHandler)

    def get_driver(self):
        return self.driver

    def get_logger(self):
        return self.logger
