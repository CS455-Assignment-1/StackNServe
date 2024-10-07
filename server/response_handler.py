import json

def send_response(handler, status_code, content_type, data, is_json=False):
    handler.send_response(status_code)
    handler.send_header('Content-type', content_type)
    handler.send_header('Access-Control-Allow-Origin', '*')
    handler.end_headers()
    
    if is_json:
        handler.wfile.write(json.dumps(data).encode('utf-8'))
    else:
        handler.wfile.write(str(data).encode('utf-8'))