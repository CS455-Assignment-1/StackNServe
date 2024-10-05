#!python

from http.server import BaseHTTPRequestHandler, HTTPServer
import sys
sys.path.insert(0, "/opt/homebrew/lib/python3.11/site-packages")
from pymongo import MongoClient

# Add the MongoDB connection string, and create collections
client = MongoClient("mongodb+srv://StackNServe:aditi_kushagra@cluster0.rvwjh.mongodb.net/")

BurgerDB = client["burger"]
BunBurgerColection = BurgerDB["bun"]
PattyBurgerColection = BurgerDB["patty"]
ToppingsBurgerColection = BurgerDB["toppings"]
SaucesBurgerColection = BurgerDB["sauces"]

# Check connection
print(client.list_database_names())
print(BurgerDB.list_collection_names())
chicken_patty_price = PattyBurgerColection.find_one({"Name": "Chicken Patty"})["Price"]
print(chicken_patty_price)
num_sauces = SaucesBurgerColection.count_documents({})
print(num_sauces)
jalapenos_description = ToppingsBurgerColection.find_one({"Name": "Jalapenos"})["Description"]
print(jalapenos_description)

class SimpleHTTPRequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        if self.path == "/initialBalance":
            self.send_response(200)
            self.send_header('Content-type', 'text/html')
            self.send_header('Access-Control-Allow-Origin', '*')  
            self.end_headers()
            self.wfile.write(b"150")  
        else:
            self.send_response(400)
            self.end_headers()

    def do_OPTIONS(self):
        self.send_response(200)
        self.send_header('Access-Control-Allow-Origin', '*')
        self.send_header('Access-Control-Allow-Methods', 'GET, OPTIONS')
        self.send_header('Access-Control-Allow-Headers', 'Content-Type')
        self.end_headers()

def run(server_class=HTTPServer, handler_class=SimpleHTTPRequestHandler, port=8000):
    server_address = ('', port)
    httpd = server_class(server_address, handler_class)
    print(f'Server running on port {port}...')
    httpd.serve_forever()

if __name__ == "__main__":
    run()

