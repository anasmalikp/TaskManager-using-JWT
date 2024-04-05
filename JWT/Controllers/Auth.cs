using JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUser _users;
        public Auth(IConfiguration config, IUser user)
        {

            _config = config;
            _users = user;

        }

        [Authorize(Roles = "admin")]
        [HttpGet(Name ="getall")]
        public IActionResult getall()
        {
            return Ok(_users.getuser());
        }


        [HttpPost("register")]
        public IActionResult register([FromBody] User newuser)
        {
            if (newuser == null)
            {
                return BadRequest("invalid properties");
            };
            var isExist = _users.getuser().FirstOrDefault(x=> x.Id == newuser.Id);
            if(isExist != null)
            {
                return BadRequest("User already registered, please login");
            }
            newuser.Id = _users.getuser().LastOrDefault().Id + 1;
            _users.register(newuser);
            return CreatedAtRoute("getuser", new {Id =  newuser.Id}, newuser);
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] Login user)
        {
            try
            {
                if(user == null)
                {
                    return BadRequest("please give proper informations");
                }
                var checkuser = _users.login(user);

                if(checkuser == null)
                {
                    return Unauthorized("invalid username or password");
                }

                string token = Gettoken(checkuser);

                return Ok(new { Token = token });
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"internal server error : {ex.Message}");
            }
        }
        

        private string Gettoken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim (ClaimTypes.Name, user.Username),
                new Claim (ClaimTypes.Role, user.role)
            };

            var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddDays(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
    }
}
