using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tango.Employee.DTOs;

namespace Tango.Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class LoginController : ControllerBase
    {
        protected readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public ActionResult Login(LoginDTO data)
        {
            if(data.Username != "Test" || data.Password != "Test@123")
            {
                return BadRequest("Invalid credentials");
            }

            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("JWTSecretKey")!);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, data.Username),
                    new Claim(ClaimTypes.Role, "Developer")
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenGenerated = tokenHandler.WriteToken(token);

            LoginResponseDTO response = new()
            {
                Username = data.Username,
                Token = tokenGenerated
            };

            return Ok(response);
        }
    }
}
