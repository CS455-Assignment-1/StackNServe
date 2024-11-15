import pytest
from pymongo import MongoClient
import requests
import json
import time

# Configuration
MONGO_URI = "mongodb+srv://StackNServe:aditi_kushagra@cluster0.rvwjh.mongodb.net/"
USER_DB_NAME = "user"
BURGER_DB_NAME = "burger"

API_BASE_URL = "http://localhost:8000"

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

@pytest.fixture(autouse=True)
def clear_user_collection(user_collection):
    # Ensure the collection is clear before each test to maintain isolation
    user_collection.delete_many({})

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