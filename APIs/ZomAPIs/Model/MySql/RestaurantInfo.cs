using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;


namespace ZomAPIs.Model.MySql
{
    [JsonObject(IsReference = true)]
    public class RestaurantInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResInfoId { get; set; }
        public string RestaurantMongoId { get; set; }
        public long RestaurantHashId { get; set; }
        
        public virtual IList<UserRestaurant> UserRestaurants { get; set; }
    }
}