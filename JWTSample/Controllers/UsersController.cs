using AutoMapper;
using JWTSample.Entities;
using JWTSample.Filters;
using JWTSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSample.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<ApplicationUser> _userMgr;

        public UsersController(UserManager<ApplicationUser> userMgr)
        {
            _userMgr = userMgr;
        }

        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto userModel)
        {
            if(userModel == null)
                return BadRequest();

            var userIdentity = Mapper.Map<ApplicationUser>(userModel);

            var result = await _userMgr.CreateAsync(userIdentity, userModel.Password);

            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok("You have been authorized");
        }
    }
}
