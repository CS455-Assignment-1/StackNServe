# post_handlers.py

import json
from database import DetailsUserCollection

def handle_update_score(handler):
    content_length = int(handler.headers['Content-Length'])
    post_data = handler.rfile.read(content_length)
    data = json.loads(post_data)
    player_id = data["player_id"]
    score = data["score"]
    DetailsUserCollection.update_one({"ID": player_id}, {"$set": {"Score": score}})
    handler.send_response(200)
    handler.send_header('Content-type', 'text/plain')
    handler.send_header('Access-Control-Allow-Origin', '*')
    handler.end_headers()
    handler.wfile.write(b"Score updated")

def handle_fetch_score(handler):
    content_length = int(handler.headers['Content-Length'])
    post_data = handler.rfile.read(content_length)
    data = json.loads(post_data)
    player_id = data["player_id"]
    player = DetailsUserCollection.find_one({"ID": player_id})
    print(player)
    score = player["Score"]
    print(score)
    handler.send_response(200)
    handler.send_header('Content-type', 'text/plain')
    handler.send_header('Access-Control-Allow-Origin', '*')
    handler.end_headers()
    handler.wfile.write(str(score).encode())
