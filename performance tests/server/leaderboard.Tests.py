from locust import HttpUser, TaskSet, task, between, constant

class LeaderboardTestUser(TaskSet):
    # Check for the leaderboard page
    @task
    def fetch_leaderboard(self):
        # Fetch the leaderboard
        self.client.get("/fetchLeaderboard")

class GameUser(HttpUser):
    host = "https://stacknserve.onrender.com"
    tasks = [LeaderboardTestUser]