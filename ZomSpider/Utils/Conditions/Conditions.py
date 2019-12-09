from selenium.webdriver.chrome.webdriver import WebDriver


class RestaurantsExistInCollection(object):

    def __init__(self, locator, x_path):
        self.locator = locator
        self.x_path = x_path

    def __call__(self, driver: WebDriver):

        elements = driver.find_elements_by_xpath(self.x_path)

        if len(elements) > 0:
            return elements
        else:
            return False
