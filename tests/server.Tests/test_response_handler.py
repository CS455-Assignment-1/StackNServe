import pytest
import requests
from pytest_httpserver import HTTPServer

def test_send_response_plain_text(httpserver: HTTPServer):
    httpserver.expect_request("/test_plain_text").respond_with_data("Success", content_type="text/plain")
    
    response = requests.get(httpserver.url_for("/test_plain_text"))
    
    assert response.status_code == 200
    assert response.headers['Content-Type'] == 'text/plain'
    assert response.text == "Success"


def test_send_response_json(httpserver: HTTPServer):
    data = {"key": "value"}
    
    httpserver.expect_request("/test_json").respond_with_json(data)
    
    response = requests.get(httpserver.url_for("/test_json"))
    
    assert response.status_code == 200
    assert response.headers['Content-Type'] == 'application/json'
    assert response.json() == data


def test_send_response_404(httpserver: HTTPServer):
    httpserver.expect_request("/not_found").respond_with_data("Not Found", status=404)
    
    response = requests.get(httpserver.url_for("/not_found"))
    
    assert response.status_code == 404
    assert response.text == "Not Found"


def test_send_response_500(httpserver: HTTPServer):
    httpserver.expect_request("/server_error").respond_with_data("Internal Server Error", status=500)
    
    response = requests.get(httpserver.url_for("/server_error"))
    
    assert response.status_code == 500
    assert response.text == "Internal Server Error"
