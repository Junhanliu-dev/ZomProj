using System.Collections.Generic;

namespace ZomAPIs.Model
{
    public class User
    {

        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public Role Name { get; set; }

        public ICollection<UserRestaurant> UserRestaurants { get; set; }

    }
}