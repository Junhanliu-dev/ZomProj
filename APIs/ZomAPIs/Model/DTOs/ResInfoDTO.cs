using System.Collections.Generic;
using ZomAPIs.Model.MySql;

namespace ZomAPIs.Model.DTOs
{
    public class UserResDTO
    {
        public int UserId { get; set; }
        public string ScreenName { get; set; }

        public IEnumerable<UserRestaurantDTO> userRestaurantList { get; set; }
    }


    public class UserRestaurantDTO
    {
        public RestaurantInfoDTO RestaurantInfo { get; set; }
        
        public bool liked { get; set; }
        public int BeenThereCount { get; set; }
    }

    public class RestaurantInfoDTO
    {
        public string RestaurantMongoId { get; set; }
    }
}