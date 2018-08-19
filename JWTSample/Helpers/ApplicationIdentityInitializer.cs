using JWTSample.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSample.Helpers
{
    public class ApplicationIdentityInitializer
    {
        private UserManager<ApplicationUser> _userMgr;

        public ApplicationIdentityInitializer(UserManager<ApplicationUser> userMgr)
        {
            _userMgr = userMgr;
        }
        
        public async Task Seed()
        {
            // see campdb application
        }

    }
}
