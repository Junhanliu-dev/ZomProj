
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZomAPIs.Model.MySql
{
    public class RestaurantInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResInfoId { get; set; }
        public int RestaurantMongoId { get; set; }
        public int RestaurantHashId { get; set; }

        public ICollection<UserRestaurant> UserRestaurants { get; set; }
    }
}