from http.server import BaseHTTPRequestHandler, HTTPServer
import sys
sys.path.insert(0, '/opt/homebrew/lib/python3.11/site-packages')
from urllib.parse import urlparse, parse_qs
import requests
from urllib.parse import urlparse, urlencode
import socket

backend_servers = ["https://stacknservethirdserver.onrender.com", "https://stacknservesecondserver.onrender.com"]
# implementation of load balancer for aletrnate requests
turn = 0
class SimpleHTTPRequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        global turn
        backend_url = backend_servers[turn]
        turn = (turn + 1) % 2
        parsed_path = urlparse(self.path)
        full_url = f"{backend_url}{parsed_path.path}"
        query_params = parse_qs(parsed_path.query)
        try:
            response = requests.get(full_url,params=query_params)
            self.send_response(response.status_code)
            for header, value in response.headers.items():
                if header.lower() != "content-encoding":
                    self.send_header(header, value)
            self.send_header("Connection", "close")
            self.end_headers()
            try:
                self.wfile.write(response.content)
                self.wfile.flush()
            except (BrokenPipeError, ValueError):
                pass 
        except requests.RequestException as e:
            self.send_error(500, f"Error forwarding request: {e}")
        finally:
            self.request.shutdown(socket.SHUT_RDWR)  # Explicitly close the socket
            self.request.close()

    def do_POST(self):
        global turn
        backend_url = backend_servers[turn]
        turn = (turn + 1) % 2

        parsed_path = urlparse(self.path)
        full_url = f"{backend_url}{parsed_path.path}"

        content_length = int(self.headers['Content-Length'])
        post_data = self.rfile.read(content_length)

        try:
            response = requests.post(full_url, data=post_data, headers={'Content-Type': 'application/json'})
            self.send_response(response.status_code)
            for header, value in response.headers.items():
                if header.lower() != "content-encoding":
                    self.send_header(header, value)
            self.send_header("Connection", "close")
            self.end_headers()
            try:
                self.wfile.write(response.content)
                self.wfile.flush()
            except (BrokenPipeError, ValueError):
                pass
        except requests.RequestException as e:
            self.send_error(500, f"Error forwarding request: {e}")
        finally:
            self.request.shutdown(socket.SHUT_RDWR)  # Explicitly close the socket
            self.request.close()

    def do_OPTIONS(self):
        self.send_response(200)
        self.send_header('Content-type', 'application/json')
        self.send_header('Access-Control-Allow-Origin', '*')
        self.send_header('Access-Control-Allow-Methods', 'GET, POST, OPTIONS')
        self.send_header('Access-Control-Allow-Headers', 'Content-Type')
        self.end_headers()

def run(server_class=HTTPServer, handler_class=SimpleHTTPRequestHandler, port=8000):
    server_address = ('', port)
    httpd = server_class(server_address, handler_class)
    print(f"Load balancer running on port {port}...")
    httpd.serve_forever()

if __name__ == "__main__":
    run()

