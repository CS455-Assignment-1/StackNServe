# database.py

from pymongo import MongoClient

# Add the MongoDB connection string, and create collections
client = MongoClient("mongodb+srv://StackNServe:aditi_kushagra@cluster0.rvwjh.mongodb.net/")

BurgerDB = client["burger"]

Collections = {
    "bun": BurgerDB["bun"],
    "patty": BurgerDB["patty"],
    "toppings": BurgerDB["toppings"],
    "sauces": BurgerDB["sauces"]
}

UserDB = client["user"]
DetailsUserCollection = UserDB["details"]
