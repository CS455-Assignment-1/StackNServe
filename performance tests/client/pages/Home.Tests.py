import sys
sys.path.insert(0, '/opt/homebrew/lib/python3.11/site-packages')
import unittest
import time
from selenium import webdriver
from selenium.webdriver.common.by import By
import random
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

def generate_random_name():
    return "TestPlayer" + str(random.randint(1, 1000))

class TestHomePage(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        cls.driver = webdriver.Chrome()
        cls.driver.maximize_window()
        cls.wait = WebDriverWait(cls.driver, 10)
        cls.driver.get("https://cs455-assignment-1.github.io/StackNServe/New_Game")

    @classmethod
    def tearDownClass(cls):
        cls.driver.quit()

    def test_home_page_load(self):
        self.assertIn("StackNServe", self.driver.title)
        print("Home Page Loaded")

    def test_home_page_loadtime(self):
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/New_Game")
        end = time.time()
        self.assertLess(end - start, 3)
        print("Page load time is less than 3 seconds")

    def test_home_page_component_loadtime(self):
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/New_Game")
        start = time.time()
        self.wait.until(EC.presence_of_element_located((By.CLASS_NAME, "Player_Name_Input")))
        self.wait.until(EC.presence_of_element_located((By.CLASS_NAME, "Player_Name_Label")))
        end = time.time()
        self.assertLess(end - start, 4)
        print("Component load time is less than 4 seconds")


    def test_home_page_select_button_time(self):
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/New_Game")
        start = time.time()
        player_name_field = self.wait.until(EC.visibility_of_element_located((By.CLASS_NAME, "Player_Name_Field")))
        player_name_field.send_keys(generate_random_name())
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Button").click()
        self.wait.until(EC.presence_of_element_located((By.CLASS_NAME, "Select_Buttons")))
        self.driver.find_element(By.CLASS_NAME, "Select_Buttons")
        self.driver.find_element(By.CLASS_NAME, "BunSelect").click()
        self.driver.find_element(By.CLASS_NAME, "PattySelect").click()
        self.driver.find_element(By.CLASS_NAME, "SaucesSelect").click()
        self.driver.find_element(By.CLASS_NAME, "ToppingsSelect").click()
        end = time.time()
        self.assertLess(end - start, 4)
        print("Select button click time is less than 4 seconds")
    
    def test_home_page_select_button_description(self):
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/New_Game")
        start = time.time()
        player_name_field = self.wait.until(EC.visibility_of_element_located((By.CLASS_NAME, "Player_Name_Field")))
        player_name_field.send_keys(generate_random_name())
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Button").click()
        self.wait.until(EC.presence_of_element_located((By.CLASS_NAME, "Select_Buttons")))
        self.driver.find_element(By.CLASS_NAME, "Select_Buttons")
        self.driver.find_element(By.CLASS_NAME, "BunToggleButton").click()
        self.driver.find_element(By.CLASS_NAME, "ClickExpandMenu")
        end = time.time()
        self.assertLess(end - start, 4)
        print("Select button description click time is less than 4 seconds")


if __name__ == '__main__':
    unittest.main()