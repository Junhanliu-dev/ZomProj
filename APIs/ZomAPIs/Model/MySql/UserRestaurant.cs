using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZomAPIs.Model.MySql
{
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