import pymongo

client = pymongo.MongoClient("mongodb://192.168.86.150:27017")

restaurant_db = client["zom_proj"]

restaurant_collection = restaurant_db["restaurant"]

restaurant_collection.insert_one({"test":"tt"})

print(client.list_database_names())