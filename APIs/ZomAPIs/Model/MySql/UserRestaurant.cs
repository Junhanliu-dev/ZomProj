using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ZomAPIs.Model.MySql
{
    
    [JsonObject(IsReference = true)]
    public class UserRestaurant
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        [Column("ResInfoId")]
        public int ResInfoId { get; set; }
        public virtual RestaurantInfo RestaurantInfo { get; set; }
        
        
        public bool Liked { get; set; }
        public int BeenThereCount { get; set; }
    }
}