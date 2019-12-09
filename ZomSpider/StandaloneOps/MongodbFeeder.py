import pymongo

client = pymongo.MongoClient("mongodb://192.168.150:27017")

restaurant_db = client["restaurant"]

print(client.list_database_names())