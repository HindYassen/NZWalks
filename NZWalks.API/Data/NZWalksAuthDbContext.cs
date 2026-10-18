using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "0ca0a142-59ea-410a-a6d9-bf09a6263d4f";
            var writerId = "baaadcc1-455c-4321-aeb1-fb7560e80ccb";


            var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = readerId,
                ConcurrencyStamp = readerId,
                Name = "Reader",
                NormalizedName = "READER"
            },
            new IdentityRole
            {
                Id =writerId,
                ConcurrencyStamp = writerId,
                Name = "Writer",
                NormalizedName = "WRITER"
            }
        };
            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}