using System.Collections.Generic;

namespace ZomAPIs.Model.DTOs
{
    public class RestaurantDTO
    {
        public string Name { get; set; }
        
        public List<string> Area { get; set; }

        public string Link { get; set; }

        public List<string> Collection { get; set; }

        public List<string> Type { get; set; }

        public string PhoneNumber { get; set; }
        
        public List<string> CuisineType { get; set; }
        
        public int Cost { get; set; }
        
        public List<string> BillAcceptance { get; set; }
        
        public Dictionary<string,string> WorkingHours { get; set; }
        
        public Dictionary<string, List<string>> Menu { get; set; }
        
        public List<string> FoodImgs { get; set; }
    }
}