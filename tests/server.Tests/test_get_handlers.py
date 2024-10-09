import pytest
import requests
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

