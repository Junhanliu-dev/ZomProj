using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using ZomAPIs.Model.Data;
using ZomAPIs.Model.MySql;

namespace ZomAPIs.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private UserDbContext _userDbContext;

        public UserController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        
        // GET
        public async Task<string> Index()
        {
            var user = await _userDbContext.Users
                .Include(u => u.UserRestaurants)
                    .ThenInclude(u => u.RestaurantInfo)
                        .Where(u => u.UserId == 0)
                            .FirstOrDefaultAsync();

            return user.UserRestaurants[0].RestaurantInfo.RestaurantMongoId;
        }
    }
}