# get_handlers.py

import random
import json
from urllib.parse import urlparse, parse_qs
from database import Collections, DetailsUserCollection
from response_handler import send_response

def handle_health(handler):
    send_response(handler, 200, 'text/plain', "Healthy")
    
def handle_initial_balance(handler):
    send_response(handler, 200, 'text/plain', "100")

def handle_order_price(handler):
    price = random.randint(150, 400)
    send_response(handler, 200, 'text/plain', price)

def handle_fetch_leaderboard(handler):
    leaderboard = []
    numPlayers = 10
    players = DetailsUserCollection.find().sort("Score", -1).limit(numPlayers)
    for player in players:
        player_name = player["Name"]
        player_score = player["Score"]
        leaderboard.append({"name": player_name, "score": player_score})

    try:
        send_response(handler, 200, 'application/json', leaderboard, is_json=True)
    except BrokenPipeError:
        print("Broken pipe: Client disconnected before leaderboard data could be sent.")


def handle_order_list(handler):
    print("Order list fetched.")
    items_list = []
    bun_count = 1 
    for _ in range(bun_count):
        bunID = random.randint(1, Collections.get("bun").count_documents({}))
        bun = Collections.get("bun").find_one({"ID": bunID})
        bunName = bun["Name"]
        items_list.append(bunName)

    patty_count = random.randint(1, Collections.get("patty").count_documents({}))
    for _ in range(patty_count):
        pattyID = random.randint(1, Collections.get("patty").count_documents({}))
        patty = Collections.get("patty").find_one({"ID": pattyID})
        pattyName = patty["Name"]
        items_list.append(pattyName)
    
    toppings_count = random.randint(1, Collections.get("toppings").count_documents({}))
    for _ in range(toppings_count):
        toppingsID = random.randint(1, Collections.get("toppings").count_documents({}))
        toppings = Collections.get("toppings").find_one({"ID": toppingsID})
        toppingsName = toppings["Name"]
        items_list.append(toppingsName)
    
    sauces_count = random.randint(1, Collections.get("sauces").count_documents({}))
    for _ in range(sauces_count):
        saucesID = random.randint(1, Collections.get("sauces").count_documents({}))
        sauces = Collections.get("sauces").find_one({"ID": saucesID})
        saucesName = sauces["Name"]
        items_list.append(saucesName)

    items_list = list(set(items_list))

    send_response(handler, 200, 'application/json', items_list, is_json=True)

def handle_burger_description(handler):
    query = parse_qs(urlparse(handler.path).query)
    item_type = query.get('type', [None])[0]
    name = query.get('name', [None])[0]

    if not item_type or not name:
        handler.send_error(400, "Query parameters 'type' and 'name' are required")
        return

    collection = Collections.get(item_type)
    if collection is None:
        handler.send_error(400, f"Invalid item type: {item_type}")
        return

    item = collection.find_one({"Name": name})
    if item:
        description = item.get("Description", "Description not available")
        price = item.get("Price", 0.0)
        print(f"{item_type.capitalize()} '{name}' fetched.")

        response_data = {
            "Name": name,
            "Description": description,
            "Price": float(price),
        }

        print(response_data)

        send_response(handler, 200, 'application/json', response_data, is_json=True)
    else:
        handler.send_error(404, f"{item_type.capitalize()} '{name}' not found")