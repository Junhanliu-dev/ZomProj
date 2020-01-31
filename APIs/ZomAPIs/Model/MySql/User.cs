using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ZomAPIs.Model.MySql
{
    [JsonObject(IsReference = true)]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        public string ScreenName { get; set; }
        
        public virtual IList<UserRestaurant> UserRestaurants { get; set; }
    }
}