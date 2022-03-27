using Idea.Web.Models.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SignalRDemo.App.Models;
using SignalRDemo.Data;
using SignalRDemo.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Idea.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly AppJwtSettings appJwtSettings;

        public UsersController(
            AppDbContext appDbContext,
            IOptions<AppJwtSettings> appSettings)
        {
            this.appDbContext = appDbContext;
            this.appJwtSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AppUser userFromRequest)
        {
            var user = appDbContext.Users.SingleOrDefault(
                       user => user.Username == userFromRequest.Username 
                    && user.Password == userFromRequest.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is invalid..." });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appJwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("Username", user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                Token = tokenString
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            if (userRegister.Password != userRegister.ConfirmPassword)
            {
                return BadRequest(new { message = "Passwords do not match..." });
            }

            this.appDbContext.Users.Add(new AppUser
            {
                Username = userRegister.Username,
                Password = userRegister.Password
            });

            this.appDbContext.SaveChanges();

            return Ok();
        }
    }
}
