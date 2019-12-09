from scrapy import Item, Field


class Restaurant(Item):

    id = Field()
    name = Field()
    address = Field()
    link = Field()
    rating = Field()
    liked = Field()

    phone_number = Field()

    avg_cost = Field()

    # enum maybe cash, card or both
    bill_acceptance = Field()

    # List maybe or str
    zone = Field()

    been_there_count = Field()

    # dict maybe, from monday to sunday
    opening_hours = Field()
    # list may in multiple collection
    collection = Field()

    # list, may have multiple cuisine type (casual dining, bar)
    restaurant_type = Field()
    # list, may have multiple cuisine type (Italian, barfood)
    cuisine_type = Field()

    # max 10 pics
    # folder address
    food_image_links = Field()

    # List
    menu_img_links = Field()
