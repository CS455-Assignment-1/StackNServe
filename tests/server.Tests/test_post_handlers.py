import pytest
from unittest.mock import patch, MagicMock
import sys
sys.path.append('server')
import post_handlers
import json

@pytest.fixture
def mock_db():
    with patch('post_handlers.DetailsUserCollection') as mock_db:
        yield mock_db


@pytest.fixture
def mock_handler():
    """Fixture to mock the handler object with necessary attributes for POST requests."""
    handler = MagicMock()
    handler.headers = {'Content-Length': '100'}
    handler.rfile = MagicMock()
    handler.wfile = MagicMock()
    return handler


def test_handle_update_score(mock_handler, mock_db):
    mock_handler.rfile.read.return_value = json.dumps({"player_id": 1, "score": 150}).encode('utf-8')
    
    post_handlers.handle_update_score(mock_handler)
    
    mock_handler.send_response.assert_called_once_with(200)
    mock_handler.send_header.assert_any_call('Content-type', 'text/plain')
    mock_handler.send_header.assert_any_call('Access-Control-Allow-Origin', '*')
    mock_handler.end_headers.assert_called_once()   
    mock_handler.wfile.write.assert_called_once_with(b'Score updated')
    mock_db.update_one.assert_called_once_with({"ID": 1}, {"$set": {"Score": 150}})


def test_handle_fetch_score(mock_handler, mock_db):
    mock_handler.rfile.read.return_value = json.dumps({"player_id": 1}).encode('utf-8')
    mock_db.find_one.return_value = {"ID": 1, "Score": 200}
    
    post_handlers.handle_fetch_score(mock_handler)
    
    mock_handler.send_response.assert_called_once_with(200)
    mock_handler.send_header.assert_any_call('Content-type', 'text/plain')
    mock_handler.send_header.assert_any_call('Access-Control-Allow-Origin', '*')
    mock_handler.end_headers.assert_called_once()
    mock_handler.wfile.write.assert_called_once_with(b'200')

    mock_db.find_one.assert_called_once_with({"ID": 1})


def test_check_unique_player_name(mock_handler, mock_db):
    mock_handler.rfile.read.return_value = json.dumps({"player_name": "NewPlayer"}).encode('utf-8')
    mock_db.find_one.return_value = None
    
    post_handlers.check_unique_player_name(mock_handler)
    
    mock_handler.send_response.assert_called_once_with(200)
    mock_handler.send_header.assert_any_call('Content-type', 'text/plain')
    mock_handler.send_header.assert_any_call('Access-Control-Allow-Origin', '*')
    mock_handler.end_headers.assert_called_once()
    mock_handler.wfile.write.assert_called_once_with(b'true')

    mock_db.find_one.assert_called_once_with({"Name": "NewPlayer"})


def test_handle_create_player(mock_handler, mock_db):
    mock_handler.rfile.read.return_value = json.dumps({"player_name": "Player6"}).encode('utf-8')
    mock_db.count_documents.return_value = 5
    post_handlers.handle_create_player(mock_handler)
    
    mock_handler.send_response.assert_called_once_with(200)
    mock_handler.send_header.assert_any_call('Content-type', 'application/json')
    mock_handler.send_header.assert_any_call('Access-Control-Allow-Origin', '*')
    mock_handler.end_headers.assert_called_once()
    mock_handler.wfile.write.assert_called_once_with(json.dumps({"player_id": 6}).encode('utf-8'))

    mock_db.insert_one.assert_called_once_with({"ID": 6, "Name": "Player6", "Score": 100})

def test_handle_check_list_correct_order(mock_handler):
    # Set up the request data for a correct order
    request_data = {
        "userOrderList": ["Bun Bottom", "Parmesan Bun", "Veggie Patty", "Tomato", "Aioli"],
        "requiredOrderList": ["Parmesan Bun", "Veggie Patty", "Tomato", "Aioli"],
        "currentPlayerScore": 100,
        "orderPrice": 50
    }
    
    # Mock the read and header behavior of the handler
    mock_handler.headers = {"Content-Length": str(len(json.dumps(request_data)))}
    mock_handler.rfile.read.return_value = json.dumps(request_data).encode('utf-8')
    
    # Call the handler function
    post_handlers.handle_check_list(mock_handler)
    
    # Expected response
    expected_response = {
        'IsOrderCorrect': True,
        'FinalScore': 100,  # 100 (current score) - 50 (no making cost) + 50 (order price)
        'Message': 'Perfect Order!'
    }

    # Assert that send_response was called with the correct arguments
    mock_handler.send_response.assert_called_once_with(200)
    mock_handler.send_header.assert_any_call('Content-type', 'application/json')
    mock_handler.send_header.assert_any_call('Access-Control-Allow-Origin', '*')
    mock_handler.end_headers.assert_called_once()

    # Assert that the correct response was written
    mock_handler.wfile.write.assert_called_once_with(json.dumps(expected_response).encode('utf-8'))

def test_handle_check_list_wrong_order(mock_handler):
    # Set up the request data for a wrong order
    request_data = {
        "userOrderList": ["Bun Bottom", "Plain Bun", "Chicken Patty", "Lettuce", "Ketchup"],
        "requiredOrderList": ["Parmesan Bun", "Veggie Patty", "Tomato", "Aioli"],
        "currentPlayerScore": 100,
        "orderPrice": 50
    }

    # Mock the read and header behavior of the handler
    mock_handler.headers = {"Content-Length": str(len(json.dumps(request_data)))}
    mock_handler.rfile.read.return_value = json.dumps(request_data).encode('utf-8')

    # Call the handler function
    post_handlers.handle_check_list(mock_handler)

    # Expected response
    expected_response = {
        'IsOrderCorrect': False,
        'FinalScore': 62,  # 100 (current score) - 10 (penalty for wrong order) - 28 (making cost)
        'Message': 'Wrong Order!'
    }

    # Assert that send_response was called with the correct arguments
    mock_handler.send_response.assert_called_once_with(200)
    mock_handler.send_header.assert_any_call('Content-type', 'application/json')
    mock_handler.send_header.assert_any_call('Access-Control-Allow-Origin', '*')
    mock_handler.end_headers.assert_called_once()

    # Assert that the correct response was written
    mock_handler.wfile.write.assert_called_once_with(json.dumps(expected_response).encode('utf-8'))

def test_handle_check_list_no_order(mock_handler):
    # Set up the request data with an empty user order
    request_data = {
        "userOrderList": [],
        "requiredOrderList": ["Parmesan Bun", "Veggie Patty", "Tomato", "Aioli"],
        "currentPlayerScore": 100,
        "orderPrice": 50
    }

    # Mock the read and header behavior of the handler
    mock_handler.headers = {"Content-Length": str(len(json.dumps(request_data)))}
    mock_handler.rfile.read.return_value = json.dumps(request_data).encode('utf-8')

    # Call the handler function
    post_handlers.handle_check_list(mock_handler)

    # Expected response for no order submission
    expected_response = {
        'Message': 'No order submitted!',
        'FinalScore': 100,  # No penalty, score remains the same
        'IsOrderCorrect': False,
    }

    # Assert that send_response was called with the correct arguments
    mock_handler.send_response.assert_called_once_with(200)
    mock_handler.send_header.assert_any_call('Content-type', 'application/json')
    mock_handler.send_header.assert_any_call('Access-Control-Allow-Origin', '*')
    mock_handler.end_headers.assert_called_once()

    # Assert that the correct response was written
    mock_handler.wfile.write.assert_called_once_with(json.dumps(expected_response).encode('utf-8'))

def test_handle_check_list_internal_error(mock_handler):
    # Set up invalid data to simulate an internal server error
    invalid_request_data = {
        "invalid_key": "value"
    }

    # Mock the read and header behavior of the handler
    mock_handler.headers = {"Content-Length": str(len(json.dumps(invalid_request_data)))}
    mock_handler.rfile.read.return_value = json.dumps(invalid_request_data).encode('utf-8')

    # Call the handler function (should trigger an internal error)
    post_handlers.handle_check_list(mock_handler)

    # Assert that send_response was called with a 500 Internal Server Error
    mock_handler.send_response.assert_called_once_with(500)
    mock_handler.send_header.assert_any_call('Content-type', 'text/plain')
    mock_handler.send_header.assert_any_call('Access-Control-Allow-Origin', '*')
    mock_handler.end_headers.assert_called_once()

    # Assert that the correct response was written
    mock_handler.wfile.write.assert_called_once_with(b'Internal Server Error')