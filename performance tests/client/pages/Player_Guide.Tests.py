import sys
sys.path.insert(0, '/opt/homebrew/lib/python3.11/site-packages')
import unittest
import time
from selenium import webdriver
from selenium.webdriver.common.by import By

class TestPlayerGuide(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.driver = webdriver.Chrome()
        cls.driver.maximize_window()
        cls.driver.get("https://cs455-assignment-1.github.io/StackNServe/")
        time.sleep(3)

    @classmethod
    def tearDownClass(cls):
        cls.driver.quit()

    def test_player_guide_page_load(self):
        self.assertIn("StackNServe", self.driver.title)
        print("Player Guide Page Loaded")

    def test_player_guide_page_loadtime(self):
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/")
        end = time.time()
        self.assertLess(end - start, 1, "Page load time exceeds 2 seconds")
        print("Page load time is less than 1 seconds")

    def test_player_guide_page_component_loadtime(self):
        start = time.time()
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/")
        time.sleep(3)
        self.driver.find_element(By.CLASS_NAME, "Player_Guide")
        self.driver.find_element(By.CLASS_NAME, "Player_Guide_Heading")
        self.driver.find_element(By.CLASS_NAME, "Player_Guide_Rules")
        self.driver.find_element(By.CLASS_NAME, "Player_Guide_Intent")
        self.driver.find_element(By.CLASS_NAME, "Play_Game_Button")
        end = time.time()
        self.assertLess(end - start, 4, "Component load time exceeds 6 seconds")
        print("Component load time is less than 4 seconds")

    def test_player_guide_page_button_click(self):
        self.driver.get("https://cs455-assignment-1.github.io/StackNServe/")
        time.sleep(3)
        self.driver.find_element(By.CLASS_NAME, "Play_Again").click()
        time.sleep(3)
        self.assertEqual("https://cs455-assignment-1.github.io/StackNServe/New_Game", self.driver.current_url, "Redirection URL mismatch after button click")
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Input")
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Label")
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Field")
        self.driver.find_element(By.CLASS_NAME, "Player_Name_Button")
        print("Button click is working")

if __name__ == '__main__':
    unittest.main(verbosity=2)
