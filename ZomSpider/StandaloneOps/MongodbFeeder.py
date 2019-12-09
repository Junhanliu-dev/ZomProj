import pymongo
import json
client = pymongo.MongoClient("mongodb://192.168.86.150:27017")
restaurant_db = client["zom_proj"]
restaurant_collection = restaurant_db["restaurants"]

PATH = "/Users/junhanliu/Documents/IDEWorkspace/PyCharm/ZomSpider/restaurant.jl"

with open(PATH) as f:

    for line in f:

        line = json.loads(line)
        del line["liked"]
        del line["been_there_count"]
        restaurant_collection.insert_one(line)

    f.close()
