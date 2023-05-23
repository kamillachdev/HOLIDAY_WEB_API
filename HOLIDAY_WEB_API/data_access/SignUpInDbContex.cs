using Microsoft.EntityFrameworkCore;

namespace HOLIDAY_WEB_API.data_access
{
    public class SignUpInDbContex : DbContext
    {
        public SignUpInDbContex(DbContextOptions<SignUpInDbContex> options)
        : base(options)
        {
        }

        public DbSet<UserDetail> UserDetails { get; set; }
    }
}
