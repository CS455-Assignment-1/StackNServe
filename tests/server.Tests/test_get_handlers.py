import pytest
from unittest.mock import MagicMock
import sys
sys.path.append('server')
import get_handlers
from get_handlers import (
    handle_initial_balance, 
    handle_order_price, 
    handle_fetch_leaderboard, 
    handle_order_list, 
    handle_burger_description
)
from response_handler import send_response
from pytest_httpserver import HTTPServer

def test_handle_initial_balance(httpserver: HTTPServer):

    httpserver.expect_request("/initialBalance").respond_with_data("100")
    
    response = requests.get(httpserver.url_for("/initialBalance"))

    assert response.status_code == 200
    assert response.text == "100"

def test_handle_order_price(httpserver: HTTPServer):

    httpserver.expect_request("/orderPrice").respond_with_data("250")
    
    response = requests.get(httpserver.url_for("/orderPrice"))

    assert response.status_code == 200
    assert response.text == "250"

def test_handle_fetch_leaderboard(httpserver: HTTPServer):

    leaderboard_data = [
        {"name": "Player1", "score": 500},
        {"name": "Player2", "score": 450},
    ]
    httpserver.expect_request("/fetchLeaderboard").respond_with_json(leaderboard_data)
    
    response = requests.get(httpserver.url_for("/fetchLeaderboard"))

    assert response.status_code == 200
    assert response.json() == leaderboard_data

def test_handle_order_list(httpserver: HTTPServer):

    order_list_data = ["Parmesan Bun", "Aioli", "Tomato", "Veggie Patty"]
    httpserver.expect_request("/orderList").respond_with_json(order_list_data)
    
    response = requests.get(httpserver.url_for("/orderList"))

    assert response.status_code == 200
    assert response.json() == order_list_data

def test_handle_burger_description_missing_params(httpserver: HTTPServer):

    httpserver.expect_request("/burger/description").respond_with_data("Query parameters 'type' and 'name' are required", status=400)
    
    response = requests.get(httpserver.url_for("/burger/description"))
    
    assert response.status_code == 400
    assert response.text == "Query parameters 'type' and 'name' are required"

# Mock the `send_response` function to avoid real HTTP responses.
@pytest.fixture(autouse=True)
def mock_send_response(monkeypatch):
    mock_send = MagicMock()
    monkeypatch.setattr('get_handlers.send_response', mock_send)
    return mock_send

def test_handle_initial_balance(mock_send_response):
    handler = MagicMock()
    handle_initial_balance(handler)
    mock_send_response.assert_called_once_with(handler, 200, 'text/plain', "100")

def test_handle_order_price(mock_send_response):
    handler = MagicMock()
    handle_order_price(handler)
    mock_send_response.assert_called_once()
    args, _ = mock_send_response.call_args
    assert args[0] == handler
    assert args[1] == 200
    assert args[2] == 'text/plain'
    assert isinstance(args[3], int)  # The random price should be an integer

def test_handle_fetch_leaderboard(mock_send_response, monkeypatch):
    handler = MagicMock()
    # Mocking the database collection for leaderboard data
    mock_collection = MagicMock()
    mock_collection.find.return_value.sort.return_value.limit.return_value = [
        {"Name": "Player1", "Score": 500},
        {"Name": "Player2", "Score": 450}
    ]

    # Patch the DetailsUserCollection to return the mock collection
    monkeypatch.setattr('get_handlers.DetailsUserCollection', mock_collection)
    
    handle_fetch_leaderboard(handler)
    
    expected_data = [
        {"name": "Player1", "score": 500},
        {"name": "Player2", "score": 450},
    ]
    mock_send_response.assert_called_once_with(handler, 200, 'application/json', expected_data, is_json=True)

def test_handle_order_list(mock_send_response, monkeypatch):
    handler = MagicMock()

    mock_bun = MagicMock()
    mock_patty = MagicMock()
    mock_toppings = MagicMock()
    mock_sauces = MagicMock()

    mock_bun.count_documents.return_value = 3
    mock_patty.count_documents.return_value = 3
    mock_toppings.count_documents.return_value = 3
    mock_sauces.count_documents.return_value = 3

    mock_bun.find_one.return_value = {"Name": "Parmesan Bun"}
    mock_patty.find_one.return_value = {"Name": "Veggie Patty"}
    mock_toppings.find_one.return_value = {"Name": "Tomato"}
    mock_sauces.find_one.return_value = {"Name": "Aioli"}

    mock_collections = {
        "bun": mock_bun,
        "patty": mock_patty,
        "toppings": mock_toppings,
        "sauces": mock_sauces
    }
    monkeypatch.setattr('get_handlers.Collections', mock_collections)

    handle_order_list(handler)

    expected_data = ["Parmesan Bun", "Veggie Patty", "Tomato", "Aioli"]

    actual_data = mock_send_response.call_args[0][3]

    assert sorted(actual_data) == sorted(expected_data)

    mock_send_response.assert_called_once_with(handler, 200, 'application/json', actual_data, is_json=True)

def test_handle_burger_description_missing_params(mock_send_response):
    handler = MagicMock()
    handler.path = "/burger/description"  # Simulate the path without query parameters
    handle_burger_description(handler)
    
    handler.send_error.assert_called_once_with(400, "Query parameters 'type' and 'name' are required")

def test_handle_burger_description_valid(mock_send_response, monkeypatch):
    handler = MagicMock()
    handler.path = "/burger/description?type=bun&name=Parmesan%20Bun"
    
    # Mocking the bun collection
    mock_bun = MagicMock()
    mock_bun.find_one.return_value = {
        "Name": "Parmesan Bun",
        "Description": "A delicious parmesan crusted bun",
        "Price": 2.5
    }
    
    # Patch the 'Collections.get' method
    mock_collections = {"bun": mock_bun}
    monkeypatch.setattr('get_handlers.Collections', mock_collections)

    handle_burger_description(handler)
    
    expected_data = {
        "Name": "Parmesan Bun",
        "Description": "A delicious parmesan crusted bun",
        "Price": 2.5
    }
    mock_send_response.assert_called_once_with(handler, 200, 'application/json', expected_data, is_json=True)

def test_handle_burger_description_not_found(mock_send_response, monkeypatch):
    handler = MagicMock()
    handler.path = "/burger/description?type=bun&name=NonExistentBun"

    # Mocking an empty result for the non-existent bun
    mock_bun = MagicMock()
    mock_bun.find_one.return_value = None
    
    # Patch the 'Collections.get' method
    mock_collections = {"bun": mock_bun}
    monkeypatch.setattr('get_handlers.Collections', mock_collections)

    handle_burger_description(handler)
    
    handler.send_error.assert_called_once_with(404, "Bun 'NonExistentBun' not found")
