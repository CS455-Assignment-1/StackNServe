#!python

from http.server import BaseHTTPRequestHandler, HTTPServer
import sys
sys.path.insert(0, "/opt/homebrew/lib/python3.11/site-packages")
from pymongo import MongoClient
import random
import json

# Add the MongoDB connection string, and create collections
client = MongoClient("mongodb+srv://StackNServe:aditi_kushagra@cluster0.rvwjh.mongodb.net/")

BurgerDB = client["burger"]
BunBurgerColection = BurgerDB["bun"]
PattyBurgerColection = BurgerDB["patty"]
ToppingsBurgerColection = BurgerDB["toppings"]
SaucesBurgerColection = BurgerDB["sauces"]

UserDB = client["user"]
DetailsUserCollection = UserDB["details"]
LeaderboardUserCollection = UserDB["leaderboard"]

class SimpleHTTPRequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        if self.path == "/initialBalance":
            self.send_response(200)
            self.send_header('Content-type', 'text/html')
            self.send_header('Access-Control-Allow-Origin', '*')  
            self.end_headers()
            self.wfile.write(b"150")  
        if self.path == "/orderPrice":
            order_price = random.randint(5, 200)
            self.send_response(200)
            self.send_header('Content-type', 'text/plain')
            self.send_header('Access-Control-Allow-Origin', '*')
            self.end_headers()
            print(order_price)
            self.wfile.write(str(order_price).encode())
        if self.path == "/orderList":
            list = []
            bunIte = 1 
            for i in range(bunIte):
                bunID = random.randint(1, BunBurgerColection.count_documents({}))
                bun = BunBurgerColection.find_one({"ID": bunID})
                bunName = bun["Name"]
                list.append(bunName)

            pattyIte = random.randint(1, PattyBurgerColection.count_documents({}))
            for i in range(pattyIte):
                pattyID = random.randint(1, PattyBurgerColection.count_documents({}))
                patty = PattyBurgerColection.find_one({"ID": pattyID})
                pattyName = patty["Name"]
                list.append(pattyName)
            
            toppingsIte = random.randint(1, ToppingsBurgerColection.count_documents({}))
            for i in range(toppingsIte):
                toppingsID = random.randint(1, ToppingsBurgerColection.count_documents({}))
                toppings = ToppingsBurgerColection.find_one({"ID": toppingsID})
                toppingsName = toppings["Name"]
                list.append(toppingsName)
            
            saucesIte = random.randint(1, SaucesBurgerColection.count_documents({}))
            for i in range(saucesIte):
                saucesID = random.randint(1, SaucesBurgerColection.count_documents({}))
                sauces = SaucesBurgerColection.find_one({"ID": saucesID})
                saucesName = sauces["Name"]
                list.append(saucesName)
            self.send_response(200)
            self.send_header('Content-type', 'application/json')
            self.send_header('Access-Control-Allow-Origin', '*')
            self.end_headers()
            self.wfile.write(json.dumps(list).encode())  
        if self.path == "/createPlayer":
            player_id = DetailsUserCollection.count_documents({})+1
            player = {"ID": player_id, "Name": "Player"+str(player_id), "Score": 100}  
            DetailsUserCollection.insert_one(player)
            self.send_response(200)
            self.send_header('Content-type', 'text/plain')
            self.send_header('Access-Control-Allow-Origin', '*')
            self.end_headers()
            self.wfile.write(str(player_id).encode())
    def do_POST(self):
        if self.path == "/updateScore":
            content_length = int(self.headers['Content-Length'])
            post_data = self.rfile.read(content_length)
            data = json.loads(post_data)
            player_id = data["player_id"]
            score = data["score"]
            DetailsUserCollection.update_one({"ID": player_id}, {"$set": {"Score": score}})
            self.send_response(200)
            self.send_header('Content-type', 'text/plain')
            self.send_header('Access-Control-Allow-Origin', '*')
            self.end_headers()
            self.wfile.write(b"Score updated")
        if self.path == "/fetchScore":
            content_length = int(self.headers['Content-Length'])
            post_data = self.rfile.read(content_length)
            data = json.loads(post_data)
            player_id = data["player_id"]
            player = DetailsUserCollection.find_one({"ID": player_id})
            print(player)
            score = player["Score"]
            print(score)
            self.send_response(200)
            self.send_header('Content-type', 'text/plain')
            self.send_header('Access-Control-Allow-Origin', '*')
            self.end_headers()
            self.wfile.write(str(score).encode())
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

