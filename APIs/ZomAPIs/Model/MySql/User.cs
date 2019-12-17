using System.Collections;
using System.Collections.Generic;

namespace ZomAPIs.Model.MySql
{
    public class User
    {
        public int UserId { get; set; }
        public string FName { get; set; }
        public Role Role { get; set; }
        public string LName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ScreenName { get; set; }
        public IEnumerable<UserRestaurant> UserRestaurants { get; set; }
    }
}