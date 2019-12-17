
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZomAPIs.Model.MySql
{
    public class RestaurantInfo
    {
        [Key]
        public int ResInfoId { get; set; }
        public int RestaurantMongoId { get; set; }
        public int RestaurantHashId { get; set; }

        public IEnumerable<UserRestaurant> UserRestaurants { get; set; }
    }
}