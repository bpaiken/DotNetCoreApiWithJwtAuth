using JWTSample.Entities;
using JWTSample.Filters;
using JWTSample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTSample.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ApplicationContext _context;
        private UserManager<ApplicationUser> _userMgr;
        private IPasswordHasher<ApplicationUser> _hasher;
        private IConfiguration _config;

        public AuthController(ApplicationContext context,
            UserManager<ApplicationUser> userMgr,
            IPasswordHasher<ApplicationUser> hasher,
            IConfiguration config)
        {
            _context = context;
            _userMgr = userMgr;
            _hasher = hasher;
            _config = config;
        }

        [ValidateModel]
        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] UserForAuthenticationDto model)
        {
            var user = await _userMgr.FindByNameAsync(model.UserName);
            if (user == null)
                return BadRequest("Failed to generate token");

            if(_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
            {
                // create claims
                var userClaims = await _userMgr.GetClaimsAsync(user);
                var claims = new[]
               {
                  new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                  new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                  new Claim(JwtRegisteredClaimNames.Email, user.Email)
                }.Union(userClaims);

                // create signature
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // create token
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: creds
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return BadRequest();
        }
    }
}
