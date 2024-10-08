# post_handlers.py

import json
from database import DetailsUserCollection
from response_handler import send_response


def handle_update_score(handler):
    content_length = int(handler.headers['Content-Length'])
    post_data = handler.rfile.read(content_length)
    data = json.loads(post_data)
    player_id = data["player_id"]
    score = data["score"]
    DetailsUserCollection.update_one({"ID": player_id}, {"$set": {"Score": score}})
    send_response(handler, 200, 'text/plain', "Score updated")

def handle_fetch_score(handler):
    content_length = int(handler.headers['Content-Length'])
    post_data = handler.rfile.read(content_length)
    data = json.loads(post_data)
    player_id = data["player_id"]
    player = DetailsUserCollection.find_one({"ID": player_id})
    print(player)
    score = player["Score"]
    print(score)
    send_response(handler, 200, 'text/plain', score)

def check_unique_player_name(handler):
    content_length = int(handler.headers['Content-Length'])
    post_data = handler.rfile.read(content_length)
    post_data_dict = json.loads(post_data)
    player_name = post_data_dict.get("player_name", "Player")
    print("Request received to check unique player name:", player_name)
    player = DetailsUserCollection.find_one({"Name": player_name})
    if player:
        print("Player name already exists")
        send_response(handler, 200, 'text/plain', "false")
    else:
        print("Player name is unique")
        send_response(handler, 200, 'text/plain', "true")

def handle_create_player(handler):
    content_length = int(handler.headers['Content-Length'])
    post_data = handler.rfile.read(content_length)
    post_data_dict = json.loads(post_data)
    
    player_name = post_data_dict.get("player_name", "Player")
    print("Request received to create player with name:", player_name)
    
    player_id = DetailsUserCollection.count_documents({}) + 1
    player = {"ID": player_id, "Name": player_name, "Score": 100}
    DetailsUserCollection.insert_one(player)

    print("Player created with ID:", player_id)
    send_response(handler, 200, 'application/json', {"player_id": player_id}, is_json=True)
