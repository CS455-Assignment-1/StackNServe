# main_server.py

from http.server import BaseHTTPRequestHandler, HTTPServer
import sys
sys.path.insert(0, '/opt/homebrew/lib/python3.11/site-packages')
from urllib.parse import urlparse

import get_handlers
import post_handlers
from database import Collections, DetailsUserCollection

class SimpleHTTPRequestHandler(BaseHTTPRequestHandler):
    routes = {
        'GET': {
            '/initialBalance': get_handlers.handle_initial_balance,
            '/orderPrice': get_handlers.handle_order_price,
            '/burger/description': get_handlers.handle_burger_description,
            '/orderList': get_handlers.handle_order_list,
            # '/createPlayer': get_handlers.handle_create_player,
            '/fetchLeaderboard': get_handlers.handle_fetch_leaderboard
        },
        'POST': {
            '/updateScore': post_handlers.handle_update_score,
            '/fetchScore': post_handlers.handle_fetch_score,
            '/createPlayer': post_handlers.handle_create_player,
            '/checkUniqueName': post_handlers.check_unique_player_name,
        }
    }

    def do_GET(self):
        self.handle_method('GET')
    
    def do_POST(self):
        self.handle_method('POST')
    
    def handle_method(self, method):
        parsed_path = urlparse(self.path)
        path = parsed_path.path
        handler_func = self.routes.get(method, {}).get(path)
        if handler_func:
            try:
                handler_func(self)
            except Exception as e:
                print(f"Error handling {method} {path}: {e}")
                self.send_error(500, "Internal Server Error")
        else:
            self.send_error(404, "Endpoint not found")

    def do_OPTIONS(self):
        self.send_response(200)
        self.send_header('Access-Control-Allow-Origin', '*')
        self.send_header('Access-Control-Allow-Methods', 'GET, POST, OPTIONS')
        self.send_header('Access-Control-Allow-Headers', 'Content-Type')
        self.end_headers()

def run(server_class=HTTPServer, handler_class=SimpleHTTPRequestHandler, port=8000):
    server_address = ('', port)
    httpd = server_class(server_address, handler_class)
    print(f'Server running on port {port}...')
    httpd.serve_forever()

if __name__ == "__main__":
    run()
