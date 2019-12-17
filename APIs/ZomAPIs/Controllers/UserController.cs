using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZomAPIs.Model.Data;
using ZomAPIs.Model.MySql;

namespace ZomAPIs.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private UserDbContext _userDbContext;

        public UserController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        
        // GET
        public async Task<IEnumerable<User>> Index()
        {
            return await _userDbContext.Users.ToListAsync();
        }
    }
}