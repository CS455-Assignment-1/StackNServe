import sys
sys.path.insert(0, '/opt/homebrew/lib/python3.11/site-packages')
import unittest
import time
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

class TestLeaderboard(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        chrome_options = webdriver.ChromeOptions()
        chrome_options.add_argument('--headless=new')
        chrome_options.add_argument('--no-sandbox')
        chrome_options.add_argument('--disable-dev-shm-usage')
        chrome_options.add_argument('--disable-gpu')

        # Use specific path for ChromeDriver
        cls.driver = webdriver.Chrome(options=chrome_options)
        cls.driver.maximize_window()
        cls.wait = WebDriverWait(cls.driver, 10)
        cls.driver.get("https://cs455-assignment-1.github.io/StackNServe/Leaderboard")

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
        self.assertLess(end - start, 1, "Page load time exceeds 1 second : " + str(end - start))
        print("Page load time is less than 1 second")

    def test_leaderboard_page_component_loadtime(self):
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/Leaderboard")
        start = time.time()
        self.wait.until(EC.presence_of_element_located((By.CLASS_NAME, "Leaderboard_Page")))
        self.wait.until(EC.presence_of_element_located((By.CLASS_NAME, "Leaderboard_Heading")))
        self.wait.until(EC.presence_of_element_located((By.CLASS_NAME, "Leaderboard_Table")))
        self.wait.until(EC.presence_of_element_located((By.CLASS_NAME, "Leaderboard_Table_Header_Name")))
        self.wait.until(EC.presence_of_element_located((By.CLASS_NAME, "Leaderboard_Table_Header_Score")))
        self.wait.until(EC.presence_of_element_located((By.CLASS_NAME, "Leaderboard_Table_Data")))
        end = time.time()
        self.assertLess(end - start, 4, "Component load time exceeds 4 seconds : " + str(end - start))
        print("Component load time is less than 4 seconds")

if __name__ == '__main__':
    unittest.main(verbosity=2)
