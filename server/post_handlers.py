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
    print("Request received to fetch score for player ID:", player_id)
    player = DetailsUserCollection.find_one({"ID": player_id})
    score = player["Score"]
    send_response(handler, 200, 'text/plain', score)

def check_unique_player_name(handler):
    print("All headers:", handler.headers)  # Add this debug line
    print("Content-Length header:", handler.headers.get('Content-Length'))  # And this one
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

def handle_check_list(handler):
    try:
        print("All headers:", handler.headers)  # Add this debug line
        content_length = int(handler.headers['Content-Length'])
        post_data = handler.rfile.read(content_length)
        print(f"Received post data: {post_data}")  # Log received data
        data = json.loads(post_data.decode('utf-8'))
        print(f"Parsed JSON data: {data}")  # Log parsed JSON

        # Use correct case when accessing JSON keys
        user_order_list = data['userOrderList']
        required_order_list = data['requiredOrderList']
        current_player_score = data['currentPlayerScore']
        order_price = data['orderPrice']

        response = check_order(user_order_list, required_order_list, current_player_score, order_price)
        send_response(handler, 200, 'application/json', response, is_json=True)
        print("Response sent:", response)
    except Exception as e:
        print(f"Error processing checkList: {e}")
        send_response(handler, 500, 'text/plain', 'Internal Server Error')

def check_order(user_order_list, required_order_list, current_player_score, order_price):
    making_cost = 0
    is_order_correct = True
    score_penalty = 0

    # Check if user order list is empty
    if len(user_order_list) == 0:
        return {
            'Message': 'No order submitted!',
            'FinalScore': current_player_score,
            'IsOrderCorrect': False
        }

    # Calculate making cost based on the items
    item_prices = {
        "Plain Bun": 10, "Sesame Bun": 12, "Garlic Bun": 20, "Parmesan Bun": 25,
        "Veggie Patty": 15, "Chicken Patty": 20, "Fish Patty": 25, "Portobello Mushroom Patty": 18,
        "Avocado": 8, "Bacon": 7, "Cheese": 5, "Egg": 4, "Jalapenos": 6, "Lettuce": 3, "Onion": 4,
        "Pickles": 2, "Tomato": 5, "Aioli": 5, "BBQ Sauce": 5, "Hot Sauce": 5, "Ketchup": 5,
        "Mayo": 5, "Mustard": 5, "Ranch": 5
    }
    for item in user_order_list:
        if item != "Bun Bottom":
            making_cost += item_prices.get(item, 0)

    # Compare user order list with required order list
    if len(required_order_list) + 1 != len(user_order_list) or user_order_list[0] != "Bun Bottom":
        is_order_correct = False
        score_penalty -= 10

    for item in required_order_list:
        if item not in user_order_list:
            is_order_correct = False
            break

    final_score = current_player_score - making_cost + (order_price if is_order_correct else 0)

    return {
        'IsOrderCorrect': is_order_correct,
        'FinalScore': final_score,
        'Message': 'Perfect Order!' if is_order_correct else 'Wrong Order!'
    }

def handle_fetch_score_name(handler):
    content_length = int(handler.headers['Content-Length'])
    post_data = handler.rfile.read(content_length)
    data = json.loads(post_data)
    player_name = data["player_name"]
    print("Request received to fetch score for player name:", player_name)
    player = DetailsUserCollection.find_one({"Name": player_name})
    score = player["Score"]
    send_response(handler, 200, 'text/plain', score)