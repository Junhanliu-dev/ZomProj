using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ZomAPIs.Helpler;
using ZomAPIs.Model.Data;
using ZomAPIs.Model.MySql;

namespace ZomAPIs.Services
{
    public interface IAuthService
    {
        Task<UserAuthBody> Authenticate(string username, string password);

        IEnumerable<User> GetAll();
    }


    public class AuthService : IAuthService
    {
        private readonly AppSetting _appSettings;
        private readonly UserDbContext _userDbContext;

        public AuthService(IOptions<AppSetting> appSettings, UserDbContext userDbContext)
        {
            _appSettings = appSettings.Value;
            _userDbContext = userDbContext;
        }
        
        public async Task<UserAuthBody> Authenticate(string userName, string password)
        {
            var user = await _userDbContext.Users
                .SingleOrDefaultAsync(x => x.ScreenName == userName && x.Password == password);
            
            var tokenHandler = new JwtSecurityTokenHandler(); 
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //add identity
                    new Claim("userID",user.UserId.ToString()),
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new UserAuthBody
            {
                Id = user.UserId,
                Username = user.ScreenName,
                Token = tokenHandler.WriteToken(token),
            };
        }
        

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
    
}