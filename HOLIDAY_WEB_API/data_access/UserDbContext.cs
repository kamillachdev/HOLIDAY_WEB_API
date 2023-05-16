using HOLIDAY_WEB_API.Models;
using Microsoft.EntityFrameworkCore;

namespace HOLIDAY_WEB_API.data_access
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { set; get; }

        public DbSet<Request> Requests { set; get; }

    }
}
