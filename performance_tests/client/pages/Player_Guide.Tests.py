import sys
sys.path.insert(0, '/opt/homebrew/lib/python3.11/site-packages')
import unittest
import time
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
import random

class TestPlayerGuide(unittest.TestCase):
    # check if the page is loaded
    @classmethod
    def setUpClass(cls):
        cls.driver = webdriver.Chrome()

    def test_player_guide_page_load(self):
        self.driver = webdriver.Chrome()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/")
        time.sleep(3)
        self.assertIn("StackNServe", self.driver.title)
        print("Player Guide Page Loaded")
        self.driver.quit()

    def test_player_guide_page_loadtime(self):
        self.driver = webdriver.Chrome()
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/")
        self.driver.quit()
        end = time.time()
        self.assertLess(end - start, 2)
        print("Page load time is less than 2 seconds")

    def test_player_guide_page_component_loadtime(self):
        self.driver = webdriver.Chrome()
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/")
        time.sleep(5)
        self.driver.find_element(By.CLASS_NAME, "Player_Guide")
        self.driver.find_element(By.CLASS_NAME, "Player_Guide_Heading")
        self.driver.find_element(By.CLASS_NAME, "Player_Guide_Rules")
        self.driver.find_element(By.CLASS_NAME, "Player_Guide_Intent")
        self.driver.find_element(By.CLASS_NAME, "Play_Game_Button")
        end = time.time()
        self.assertLess(end - start, 6)
        print("Component load time is less than 6 seconds")

    def test_player_guide_page_button_click(self):
        self.driver = webdriver.Chrome()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/")
        time.sleep(15)
        self.driver.find_element(By.CLASS_NAME, "Play_Again").click()
        time.sleep(5)
        self.assertEqual("https://cs455-assignment-1.github.io/StackNServe/New_Game", self.driver.current_url)
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Input")
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Label")
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Field")
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Button")
        print("Button click is working")
        self.driver.quit()

if __name__ == '__main__':
    unittest.main()
