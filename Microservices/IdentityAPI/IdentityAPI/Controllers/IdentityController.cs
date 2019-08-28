using IdentityAPI.Infrastructure;
using IdentityAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IdentityDbContext db;
        private IConfiguration Configuration;
        public IdentityController(IdentityDbContext db,
            IConfiguration Configuration)
        {
            this.db = db;
            this.Configuration = Configuration;
        }

        [HttpPost("auth/register", Name = "Register")]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<UserInfo>> RegisterAsync(UserInfo model)
        {
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                var result = await db.Users.AddAsync(model);
                await db.SaveChangesAsync();
                return Created("", new
                {
                    Id = result.Entity.Id,
                    FirstName = result.Entity.FirstName,
                    LastName = result.Entity.LastName,
                    Email = result.Entity.Email
                });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("auth/token", Name = "GetToken")]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<dynamic> GetToken(LoginModel model)
        {
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Email == model.Email &&
                u.Password == model.Password);
                if (user == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(GenerateToken(user));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private dynamic GenerateToken(UserInfo user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Jwt:Secret")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: Configuration.GetValue<string>("Jwt:Issuer"),
                audience: Configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new
            {
                user.FirstName,
                user.LastName,
                token = tokenString
            };
        }
    }
}