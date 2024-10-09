import pytest
from unittest.mock import patch, MagicMock
import sys
sys.path.append('/Users/aditikhandelia/Desktop/IITK ACADS/client_server/server')
import post_handlers
import database
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