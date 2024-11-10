from locust import HttpUser, TaskSet, task, between

class HomeTestUser(TaskSet):
    @task
    def fetch_order_list(self):
        # Fetch the current order list
        self.client.get("/orderList")

    @task
    def fetch_order_price(self):
        # Fetch the current order price
        self.client.get("/orderPrice")

    @task
    def submit_order(self):
        # Submit an order for validation with sample data
        data = {
            "userOrderList": ["ingredient1", "ingredient2"],
            "requiredOrderList": ["ingredient1", "ingredient2"],
            "currentPlayerScore": 100,
            "orderPrice": 50
        }
        self.client.post("/checkList", json=data)

    @task
    def fetch_player_score(self):
        # Fetch the player score by ID
        data = {"player_id": 111}  # Sample player ID
        self.client.post("/fetchScore", json=data)

    @task
    def update_player_score(self):
        # Update the player's score
        data = {
            "player_id": 1,  # Sample player ID
            "score": 120
        }
        self.client.post("/updateScore", json=data)
    

class GameUser(HttpUser):
    wait = between(10, 15)
    host = "https://stacknserve.onrender.com"
    tasks = [HomeTestUser]

