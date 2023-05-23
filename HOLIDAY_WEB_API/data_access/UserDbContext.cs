using HOLIDAY_WEB_API.Models;
using Microsoft.EntityFrameworkCore;

namespace HOLIDAY_WEB_API.data_access
{
    public class UserDbContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public UserDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { set; get; }

        public DbSet<Request> Requests { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = _configuration["ConnectionStrings:UserConnection"];
            if (conn == null) {
                throw new Exception("Connection string is null");
            }

            optionsBuilder.UseSqlServer(conn);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
