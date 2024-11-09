import sys
sys.path.insert(0, '/opt/homebrew/lib/python3.11/site-packages')
import unittest
import time
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
import random

class TestLeaderboard(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        cls.driver = webdriver.Chrome()

    def test_leaderboard_page_load(self):
        self.driver = webdriver.Chrome()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/Leaderboard")
        time.sleep(3)
        self.assertIn("StackNServe", self.driver.title)
        print("Leaderboard Page Loaded")
        self.driver.quit()
    
    def test_leaderboard_page_loadtime(self):
        self.driver = webdriver.Chrome()
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/Leaderboard")
        self.driver.quit()
        end = time.time()
        self.assertLess(end - start, 2)
        print("Page load time is less than 2 seconds")

    def test_leaderboard_page_component_loadtime(self):
        self.driver = webdriver.Chrome()
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/Leaderboard")
        time.sleep(5)
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Page")
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Heading")
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Table")
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Table_Header_Name")
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Table_Header_Score")
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Table_Data")
        end = time.time()
        self.assertLess(end - start, 6)
        print("Component load time is less than 6 seconds")

if __name__ == '__main__':
    unittest.main()