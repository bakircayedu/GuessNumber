using GuessNumber.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GuessNumber.Models;

namespace GuessNumber.Data
{
    public class AuthDbContext : IdentityDbContext<GuessNumberUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-JKA3N2L;Database=GuessNumber;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<GuessNumber.Models.MatchRequest> MatchRequest { get; set; }

        public DbSet<GuessNumber.Models.MatchResponse> MatchResponse { get; set; }

        public DbSet<GuessNumber.Models.GamePlayMove> GamePlayMove { get; set; }

        
    }
}


