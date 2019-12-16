using Microsoft.EntityFrameworkCore;

namespace ZomAPIs.Model.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RestaurantInfo> RestaurantInfo { get; set; }
        public DbSet<UserRestaurant>  UserRestaurants { get; set; }
    }
}