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
        public string RestaurantMongoId { get; set; }
        public long RestaurantHashId { get; set; }

        public IList<UserRestaurant> UserRestaurants { get; set; }
    }
}