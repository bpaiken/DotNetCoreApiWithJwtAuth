using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTSample.Entities
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            return;
        }

        // Not sure this will work - need to check on creating user with identity
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<ApplicationUser>().HasData(
        //        new ApplicationUser
        //        {
        //            UserName = "bpaiken",
        //            FirstName = "Brian",
        //            LastName = "Aiken",
        //            Email = "hello@gmail.com"
        //        }
        //        );
        //}
    }
}
