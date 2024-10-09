import requests
from pytest_httpserver import HTTPServer
import pytest


def test_get_routes(httpserver: HTTPServer):
    httpserver.expect_request("/initialBalance").respond_with_data("100")
    httpserver.expect_request("/orderPrice").respond_with_data("100")
    httpserver.expect_request("/burger/description").respond_with_data("Lettuce, Tomato, Cheese")
    httpserver.expect_request("/orderList").respond_with_data("Order List")
    httpserver.expect_request("/fetchLeaderboard").respond_with_data("Leaderboard Data")

    response = requests.get(httpserver.url_for("/initialBalance"))
    
    assert response.status_code == 200
    assert response.text == "100"

    response = requests.get(httpserver.url_for("/orderPrice"))

    assert response.status_code == 200
    assert response.text == "100"

    response = requests.get(httpserver.url_for("/burger/description"))

    assert response.status_code == 200
    assert response.text == "Lettuce, Tomato, Cheese"

    response = requests.get(httpserver.url_for("/orderList"))

    assert response.status_code == 200
    assert response.text == "Order List"

    response = requests.get(httpserver.url_for("/fetchLeaderboard"))

    assert response.status_code == 200
    assert response.text == "Leaderboard Data"


def test_post_routes(httpserver: HTTPServer):
    httpserver.expect_request("/updateScore", method="POST").respond_with_data("Score Updated")
    httpserver.expect_request("/fetchScore", method="POST").respond_with_data("Fetched Score")
    httpserver.expect_request("/createPlayer", method="POST").respond_with_data("Player Created")
    httpserver.expect_request("/checkUniqueName", method="POST").respond_with_data("Unique Name Checked")

    response = requests.post(httpserver.url_for("/updateScore"), data={})

    assert response.status_code == 200
    assert response.text == "Score Updated"

    response = requests.post(httpserver.url_for("/fetchScore"), data={})

    assert response.status_code == 200
    assert response.text == "Fetched Score"

    response = requests.post(httpserver.url_for("/createPlayer"), data={})

    assert response.status_code == 200
    assert response.text == "Player Created"

    response = requests.post(httpserver.url_for("/checkUniqueName"), data={})

    assert response.status_code == 200
    assert response.text == "Unique Name Checked"

def test_non_existing_route(httpserver: HTTPServer):
    httpserver.expect_request("/nonExistentRoute").respond_with_data("Not Found", status=404)

    response = requests.get(httpserver.url_for("/nonExistentRoute"))

    assert response.status_code == 404
    assert response.text == "Not Found"

def test_options_request(httpserver: HTTPServer):
    httpserver.expect_request("/anyRoute", method="OPTIONS").respond_with_data("", headers={
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, POST, OPTIONS',
        'Access-Control-Allow-Headers': 'Content-Type'
    })

    response = requests.options(httpserver.url_for("/anyRoute"))

    assert response.status_code == 200
    assert response.headers['Access-Control-Allow-Origin'] == '*'
    assert response.headers['Access-Control-Allow-Methods'] == 'GET, POST, OPTIONS'
    assert response.headers['Access-Control-Allow-Headers'] == 'Content-Type'
