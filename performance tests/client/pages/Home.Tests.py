import sys
sys.path.insert(0, '/opt/homebrew/lib/python3.11/site-packages')
import unittest
import time
from selenium import webdriver
from selenium.webdriver.common.by import By
import random

def generate_random_name():
    return "TestPlayer" + str(random.randint(1, 1000))

class TestHomePage(unittest.TestCase):

    @classmethod
    def setUpClass(cls):
        cls.driver = webdriver.Chrome()
        cls.driver.maximize_window()
        cls.driver.get("https://cs455-assignment-1.github.io/StackNServe/New_Game")
        time.sleep(10)

    def test_home_page_load(self):
        self.assertIn("StackNServe", self.driver.title)
        print("Home Page Loaded")

    def test_home_page_loadtime(self):
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/New_Game")
        end = time.time()
        self.assertLess(end - start, 1)
        print("Page load time is less than 1 second")

    def test_home_page_component_loadtime(self):
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/New_Game")
        time.sleep(2)
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Input")
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Label")
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Field")
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Button")
        end = time.time()
        self.assertLess(end - start, 3)
        print("Component load time is less than 3 seconds")

    def test_home_page_button_click(self):
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/New_Game")
        time.sleep(3)
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Field").send_keys(generate_random_name())
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Button").click()
        time.sleep(3)
        self.assertEqual("https://cs455-assignment-1.github.io/StackNServe/New_Game", self.driver.current_url)
        self.driver.find_element(By.CLASS_NAME, "Timer_Divison")
        self.driver.find_element(By.CLASS_NAME, "Cooking_Table_Division")
        self.driver.find_element(By.CLASS_NAME, "Order_Division")
        self.driver.find_element(By.CLASS_NAME, "Skip_Play_Buttons")
        self.driver.find_element(By.CLASS_NAME, "Score_Board_Divison")
        print("Button click functionality works as expected")

    @classmethod
    def tearDownClass(cls):
        cls.driver.quit()

if __name__ == '__main__':
    unittest.main()
