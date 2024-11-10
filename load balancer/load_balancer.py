from http.server import BaseHTTPRequestHandler, HTTPServer
import sys
sys.path.insert(0, '/opt/homebrew/lib/python3.11/site-packages')
from urllib.parse import urlparse, parse_qs
import requests
from urllib.parse import urlparse, urlencode
import socket
import threading
import time

primary_servers = ["https://stacknservesecondserver.onrender.com", "https://stacknservethirdserver.onrender.com"]
backup_servers = ["https://stacknservebackupserver.onrender.com"]

backend_servers = primary_servers + backup_servers

# Implementation of load balancer for alternate requests
turn = 0

lock = threading.Lock()

def health_check():
    global backend_servers
    while True:
        run_health_check()
        time.sleep(10)  # Run health check every 10 seconds

def run_health_check():
    global backend_servers
    healthy_servers = []
    for server in primary_servers + backup_servers:
        try:
            response = requests.get(f"{server}/health", timeout=2)
            if response.status_code == 200:
                healthy_servers.append(server)
        except requests.RequestException:
            print(f"Server {server} is not reachable.")
    with lock:
        backend_servers = healthy_servers if healthy_servers else backup_servers

def immediate_health_check():
    threading.Thread(target=run_health_check, daemon=True).start()

class SimpleHTTPRequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        global turn
        attempt = 0
        max_attempts = len(backend_servers)
        while attempt < max_attempts:
            with lock:
                if not backend_servers:
                    self.send_error(500, "No backend servers available")
                    return
                backend_url = backend_servers[turn % len(backend_servers)]
                turn = (turn + 1) % len(backend_servers)
            parsed_path = urlparse(self.path)
            full_url = f"{backend_url}{parsed_path.path}"
            query_params = parse_qs(parsed_path.query)
            try:
                # Set a timeout for the request to the backend server
                response = requests.get(full_url, params=query_params, timeout=5)
                
                # Forward the response status and headers to the client
                self.send_response(response.status_code)
                for header, value in response.headers.items():
                    if header.lower() != "content-encoding":
                        self.send_header(header, value)
                self.send_header("Connection", "close")
                self.end_headers()
                
                # Attempt to write the response content to the client
                try:
                    self.wfile.write(response.content)
                    self.wfile.flush()
                except (BrokenPipeError, ConnectionResetError) as e:
                    print(f"Client disconnected: {e}")
                return
            except requests.RequestException as e:
                print(f"Request to backend server failed: {e}")
                attempt += 1
                immediate_health_check()  # Run health check immediately if request fails
        self.send_error(500, "All backend servers are unavailable.")

    def do_POST(self):
        global turn
        attempt = 0
        max_attempts = len(backend_servers)
        while attempt < max_attempts:
            with lock:
                if not backend_servers:
                    self.send_error(500, "No backend servers available")
                    return
                backend_url = backend_servers[turn % len(backend_servers)]
                turn = (turn + 1) % len(backend_servers)
            
            parsed_path = urlparse(self.path)
            full_url = f"{backend_url}{parsed_path.path}"

            content_length = int(self.headers['Content-Length'])
            post_data = self.rfile.read(content_length)

            try:
                # Set a timeout for the request to the backend server
                response = requests.post(full_url, data=post_data, headers={'Content-Type': 'application/json'}, timeout=5)
                
                # Forward the response status and headers to the client
                self.send_response(response.status_code)
                for header, value in response.headers.items():
                    if header.lower() != "content-encoding":
                        self.send_header(header, value)
                self.send_header("Connection", "close")
                self.end_headers()

                # Attempt to write the response content to the client
                try:
                    self.wfile.write(response.content)
                    self.wfile.flush()
                except (BrokenPipeError, ConnectionResetError) as e:
                    print(f"Client disconnected: {e}")
                return
            except requests.RequestException as e:
                print(f"Request to backend server failed: {e}")
                attempt += 1
                immediate_health_check()  # Run health check immediately if request fails
        self.send_error(500, "All backend servers are unavailable.")

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
    threading.Thread(target=health_check, daemon=True).start()
    httpd.serve_forever()

if __name__ == "__main__":
    run()
