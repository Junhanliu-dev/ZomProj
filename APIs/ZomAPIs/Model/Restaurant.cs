using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ZomAPIs.Model
{
    public class Restaurant
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        
        [BsonElement("id")]
        public Int64 Id { get; set; }
        
        [BsonElement("rating")]
        public double Rating { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("zone")]
        public List<string> Area { get; set; }
        
        [BsonElement("link")] 
        public string Link { get; set; }
        
        [BsonElement("collection")]
        public List<string> Collection { get; set; }
        
        [BsonElement("restaurant_type")]
        public List<string> Type { get; set; }
        
        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; }
        
        [BsonElement("cuisine_type")]
        public List<string> CuisineType { get; set; }

        [BsonElement("address")] 
        public string Address { get; set; }
        
        [BsonElement("avg_cost")]
        public int Cost { get; set; }
        
        [BsonElement("bill_acceptance")] 
        public List<string> BillAcceptance { get; set; }
        
        [BsonElement("opening_hours")]
        public Dictionary<string,string> WorkingHours { get; set; }
        
        [BsonElement("menu_img_links")]
        public Dictionary<string, List<string>> Menu { get; set; }
        
        [BsonElement("food_image_links")]
        public List<string> FoodImgs { get; set; }
        
    }
}