using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MongoDB.Bson;
using ZomAPIs.Model;
using ZomAPIs.Model.Data;
using ZomAPIs.Model.DTOs;
using ZomAPIs.Model.MySql;
using ZomAPIs.Services;

namespace ZomAPIs.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserDbContext _userDbContext;
        private IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private IAuthService _authService;

        public UserController(UserDbContext userDbContext, IRestaurantRepository restaurantRepository, IMapper mapper, IAuthService authService)
        {
            _userDbContext = userDbContext;

            _restaurantRepository = restaurantRepository;

            _mapper = mapper;
            _authService = authService;
        }
        
        // GET
        public async Task<IEnumerable<User>> Index()
        {
            return await _userDbContext.Users.ToListAsync();
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<UserResDTO> GetUserById(int id)
        {
            var result = await _userDbContext.Users
                .Include(ur => ur.UserRestaurants)
                .ThenInclude(urInfo => urInfo.RestaurantInfo)
                .Where(user => user.UserId == id)
                .FirstAsync();

            return _mapper.Map<User,UserResDTO>(result);
        }
        
        [HttpPost("authenticate")]
        public async Task<UserAuthBody> Authenticate([FromBody]Customer userParam)
        {
            var userAuth = await _authService.Authenticate(userParam.Username, userParam.Password);
            return userAuth;
        }
    }
}