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

        public UserDbContext(DbContextOptions<UserDbContext> options): base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserRestaurant>()
                .HasKey(k => new {k.UserId, k.ResInfoId});

            modelBuilder.Entity<User>(
                e => 
                    e.Property(u => u.Role)
                        .HasConversion(x => x.ToString(),    // to converter
                        x=> (Role)Enum.Parse(typeof(Role),x))); // from converter
            
            //M-N relationship between User and UserRestaurantInfo
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