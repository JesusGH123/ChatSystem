from test import test,run
from selenium.webdriver.common.keys import Keys

@run
@test('https://www.python.org/')
def loginTest(driver):
    assert "Python" in driver.title
    elem = driver.find_element_by_name("q")
    elem.clear()
    elem.send_keys("pycon")
    elem.send_keys(Keys.RETURN)
    assert "No results found." not in driver.page_source