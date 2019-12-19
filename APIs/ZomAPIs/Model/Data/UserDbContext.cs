using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using ZomAPIs.Model.MySql;

namespace ZomAPIs.Model.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RestaurantInfo> RestaurantInfos { get; set; }

        public DbSet<UserRestaurant> UserRestaurants { get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options): base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserRestaurant>()
                .HasKey(k => new {k.UserId, k.ResInfoId});

            modelBuilder.Entity<User>(
                e =>
                    e.Property(u => u.Role)
                        .HasConversion(x => x.ToString(),
                            x => (Role) Enum.Parse(typeof(Role), x)));

            modelBuilder.Entity<User>().HasData(new User
            {
                FName = "Junhan",
                LName = "Liu",
                Role = Role.Manager,
                Password = "Seawave@007",
                Email = "junhanliu.leon@gmail.com",
                ScreenName = "Zayhan"
            });

            modelBuilder.Entity<RestaurantInfo>()
                .HasData(new RestaurantInfo
                {
                    RestaurantMongoId = "5dede56b4a22d82ed1e66095",
                    RestaurantHashId = 7656439828,
                });

            modelBuilder.Entity<UserRestaurant>()
                .HasData(new UserRestaurant
                {
                    UserId = 0,
                    ResInfoId = 0,
                    Liked = true,
                    BeenThereCount = 1,
                });
            
                modelBuilder.Entity<UserRestaurant>()
                    .HasOne(p => p.User)
                    .WithMany(p => p.UserRestaurants)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<UserRestaurant>()
                    .HasOne(P => P.RestaurantInfo)
                    .WithMany(p => p.UserRestaurants)
                    .HasForeignKey(k => k.ResInfoId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
        
    }
}