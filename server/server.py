from http.server import HTTPServer, BaseHTTPRequestHandler
import json

class SimpleHTTPRequestHandler(BaseHTTPRequestHandler):
    
    def _set_headers(self):
        self.send_response(200)
        self.send_header('Content-type', 'application/json')
        self.send_header('Access-Control-Allow-Origin', '*')  # Allow Blazor client to connect
        self.end_headers()

    # Handle GET requests
    def do_GET(self):
        self._set_headers()
        message = {"message": "Server is running and connected!"}
        self.wfile.write(json.dumps(message).encode('utf-8'))

    # Handle POST requests (for testing POST request connectivity)
    def do_POST(self):
        self._set_headers()
        response = {"message": "POST request received and connected!"}
        self.wfile.write(json.dumps(response).encode('utf-8'))

if __name__ == "__main__":
    httpd = HTTPServer(('', 8000), SimpleHTTPRequestHandler)
    print("Python server running on port 8000...")
    httpd.serve_forever()
