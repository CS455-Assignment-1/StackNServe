import sys
sys.path.insert(0, '/opt/homebrew/lib/python3.11/site-packages')
import unittest
import time
from selenium import webdriver
from selenium.webdriver.common.by import By

class TestLeaderboard(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        cls.driver = webdriver.Chrome()
        cls.driver.maximize_window()
        cls.driver.get("https://cs455-assignment-1.github.io/StackNServe/Leaderboard")
        time.sleep(3)

    @classmethod
    def tearDownClass(cls):
        cls.driver.quit()

    def test_leaderboard_page_load(self):
        self.assertIn("StackNServe", self.driver.title)
        print("Leaderboard Page Loaded")

    def test_leaderboard_page_loadtime(self):
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/Leaderboard")
        end = time.time()
        self.assertLess(end - start, 1, "Page load time exceeds 1 second")
        print("Page load time is less than 1 second")

    def test_leaderboard_page_component_loadtime(self):
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/Leaderboard")
        time.sleep(3)
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Page")
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Heading")
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Table")
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Table_Header_Name")
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Table_Header_Score")
        self.driver.find_element(By.CLASS_NAME, "Leaderboard_Table_Data")
        end = time.time()
        self.assertLess(end - start, 4, "Component load time exceeds 6 seconds")
        print("Component load time is less than 4 seconds")

if __name__ == '__main__':
    unittest.main(verbosity=2)
