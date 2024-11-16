import pytest
from pymongo import MongoClient
import requests
import json
import time

# Configuration
MONGO_URI = "mongodb+srv://StackNServe:aditi_kushagra@cluster0.rvwjh.mongodb.net/"
USER_DB_NAME = "user"
BURGER_DB_NAME = "burger"

API_BASE_URL = "https://stacknserve.onrender.com"

@pytest.fixture
def mongo_client():
    client = MongoClient(MONGO_URI)
    yield client
    client.close()

@pytest.fixture
def user_db(mongo_client):
    database = mongo_client[USER_DB_NAME]
    return database

@pytest.fixture
def user_collection(user_db):
    collection = user_db["details"]
    return collection

def test_create_player(user_collection):
    player_name = "Test Player"
    payload = {
        "player_name": player_name
    }

    response = requests.post(f"{API_BASE_URL}/createPlayer", json=payload)

    assert response.status_code == 200
    response_data = response.json()
    assert "player_id" in response_data

    # Verify in the database
    player_id = response_data["player_id"]
    player = user_collection.find_one({"ID": player_id})
    assert player is not None
    assert player["Name"] == player_name
    assert player["Score"] == 100  # Default score

    # Delete the player after the assertions
    user_collection.delete_one({"ID": player_id})

def test_player_exists(user_collection):
    player_name = f"Unique_Player_{int(time.time())}"
    payload = {
        "player_name": player_name
    }
    create_response = requests.post(f"{API_BASE_URL}/createPlayer", json=payload)
    assert create_response.status_code == 200
    response_data = create_response.json()
    player_id = response_data["player_id"]

    check_payload = {
        "player_name": player_name
    }
    response = requests.post(f"{API_BASE_URL}/checkUniqueName", json=check_payload)

    assert response.status_code == 200
    assert response.text == "false" 

    user_collection.delete_one({"ID": player_id})

def test_player_does_not_exist(user_collection):
    player_name = f"Unique_Player_{int(time.time())}"
    payload = {
        "player_name": player_name
    }

    response = requests.post(f"{API_BASE_URL}/checkUniqueName", json=payload)

    assert response.status_code == 200
    assert response.text == "true"

def test_update_score(user_collection):
    player_name = f"Test_Player_{int(time.time())}"
    payload = {
        "player_name": player_name
    }
    create_response = requests.post(f"{API_BASE_URL}/createPlayer", json=payload)
    assert create_response.status_code == 200
    response_data = create_response.json()
    player_id = response_data["player_id"]

    # Update the score
    update_payload = {
        "player_id": player_id,
        "score": 150
    }
    update_response = requests.post(f"{API_BASE_URL}/updateScore", json=update_payload)

    assert update_response.status_code == 200
    assert update_response.text == "Score updated"

    # Fetch the score from the database
    player = user_collection.find_one({"ID": player_id})
    assert player is not None
    assert player["Score"] == 150


    # Delete the player after the assertions
    user_collection.delete_one({"ID": player_id})

def test_fetch_score(user_collection):
    player_name = f"Test_Player_{int(time.time())}"
    payload = {
        "player_name": player_name
    }
    create_response = requests.post(f"{API_BASE_URL}/createPlayer", json=payload)
    assert create_response.status_code == 200
    response_data = create_response.json()
    player_id = response_data["player_id"]

    # Fetch the score
    fetch_payload = {
        "player_id": player_id
    }
    fetch_response = requests.post(f"{API_BASE_URL}/fetchScore", json=fetch_payload)

    assert fetch_response.status_code == 200
    response_data = fetch_response.json()
    assert response_data == 100

    # Delete the player after the assertions
    user_collection.delete_one({"ID": player_id})

def test_fetch_leaderboard(user_collection):
    player_names = [f"Player_{int(time.time())}_{i}" for i in range(3)]
    player_ids = []
    scores = [150, 200, 100]

    for name, score in zip(player_names, scores):
        # Create player
        payload = {
            "player_name": name
        }
        create_response = requests.post(f"{API_BASE_URL}/createPlayer", json=payload)
        assert create_response.status_code == 200
        response_data = create_response.json()
        player_id = response_data["player_id"]
        player_ids.append(player_id)

        # Update score
        update_payload = {
            "player_id": player_id,
            "score": score
        }
        update_response = requests.post(f"{API_BASE_URL}/updateScore", json=update_payload)
        assert update_response.status_code == 200
        assert update_response.text == "Score updated"

    # Fetch the leaderboard
    response = requests.get(f"{API_BASE_URL}/fetchLeaderboard")

    assert response.status_code == 200
    leaderboard = response.json()
    assert len(leaderboard) >= 3

    # Check if the leaderboard is sorted by score in descending order
    leaderboard_scores = [player["score"] for player in leaderboard]
    assert leaderboard_scores == sorted(leaderboard_scores, reverse=True)

    # Delete the players created for this test
    for player_id in player_ids:
        user_collection.delete_one({"ID": player_id})

def test_update_leaderboard(user_collection):
    player_name = f"Test_Player_{int(time.time())}"
    payload = {
        "player_name": player_name
    }
    create_response = requests.post(f"{API_BASE_URL}/createPlayer", json=payload)
    assert create_response.status_code == 200
    response_data = create_response.json()
    player_id = response_data["player_id"]

    # Update the score
    update_payload = {
        "player_id": player_id,
        "score": 150000
    }
    update_response = requests.post(f"{API_BASE_URL}/updateScore", json=update_payload)

    assert update_response.status_code == 200
    assert update_response.text == "Score updated"

    # Fetch the leaderboard
    response = requests.get(f"{API_BASE_URL}/fetchLeaderboard")

    assert response.status_code == 200
    leaderboard = response.json()
    assert len(leaderboard) >= 1
    assert leaderboard[0]["name"] == player_name
    assert leaderboard[0]["score"] == 150000

    # Delete the player created for this test
    user_collection.delete_one({"ID": player_id})