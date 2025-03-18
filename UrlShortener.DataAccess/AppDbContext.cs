using Microsoft.EntityFrameworkCore;
using UrlShortener.DataAccess.Entities;
using UrlShortener.Service;

namespace UrlShortener.DataAccess
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UrlEntity> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().HasData(

                new UserEntity { Id = 1, Login = "admin", Role = StaticData.RoleAdmin, Password = "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAXSviaP+MhkWco44Ognl56AAAAAACAAAAAAAQZgAAAAEAACAAAACyJUHBMGlxYBZfQLUph+MG7HAoGQ6KyvzExw3ymB9kdAAAAAAOgAAAAAIAACAAAADq+Qk6o9xCSGssQRqt1R7laWl5hfsbtySjX7t1VKpwAhAAAAAD/iqqmULivfii2YSOIstNQAAAAHKfj4xisTwSw1rEF0GSRBIHgHLlJEDU0vO4XIzWCB2PKOxxf97GpgjntB80KbRVjflFHhzugrpfTUV4ilBCvJY=" }
            );
        }

    }
}
