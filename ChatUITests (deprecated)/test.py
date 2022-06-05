import functools
from selenium import webdriver
from selenium.webdriver.firefox.firefox_binary import FirefoxBinary

import json

def test(link):
    def f(test_f):
        which_driver = {
            'Firefox':lambda : webdriver.Firefox(FirefoxBinary('./geckodriver.exe'))
            ,'Chrome':lambda : webdriver.Chrome('./chromedriver.exe')
        }
        env_vars = json.loads(open('config.json.env').read())
        @functools.wraps(test_f)
        def wrapper():
            driver = which_driver[env_vars['web_driver']]()
            driver.get(link)
            test_f(driver)
            driver.close()
        return wrapper
    return f
def run(func):
    func()


