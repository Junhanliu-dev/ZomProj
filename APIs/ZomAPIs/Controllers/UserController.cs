using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using ZomAPIs.Model;
using ZomAPIs.Model.Data;
using ZomAPIs.Model.MySql;

namespace ZomAPIs.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private UserDbContext _userDbContext;
        private IRestaurantRepository _restaurantRepository;

        public UserController(UserDbContext userDbContext, IRestaurantRepository restaurantRepository)
        {
            _userDbContext = userDbContext;

            _restaurantRepository = restaurantRepository;
        }
        
        // GET
        public async Task<IEnumerable<User>> Index()
        {
            return await _userDbContext.Users.ToListAsync();
        }

        [Route("[controller]/user/{id}")]
        public async Task<User> GetUserById(int id)
        {
            var user = await _userDbContext.Users
                .Include(u => u.UserRestaurants)
                .ThenInclude(u => u.RestaurantInfo)
                .Where(u => u.UserId == id)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}