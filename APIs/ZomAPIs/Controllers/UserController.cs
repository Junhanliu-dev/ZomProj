using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZomAPIs.Model;
using ZomAPIs.Model.Data;

namespace ZomAPIs.Controllers
{
    public class UserController : Controller
    {
        private UserContext _userContext;

        public UserController(UserContext userContext)
        
        {
            _userContext = userContext;
        }
        
        // GET
        [Route("user")]
        public async Task<IEnumerable<User>> Index()
        {
            return await _userContext.Users.ToListAsync();
        }
    }
}