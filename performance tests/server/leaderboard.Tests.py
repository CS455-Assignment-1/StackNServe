from locust import HttpUser, TaskSet, task, between

class LeaderboardTestUser(TaskSet):
    # Check for the leaderboard page
    @task
    def fetch_leaderboard(self):
        # Fetch the leaderboard
        self.client.get("/fetchLeaderboard")

class GameUser(HttpUser):
    wait = between(3, 5)
    host = "http://localhost:8000"
    tasks = [LeaderboardTestUser]

