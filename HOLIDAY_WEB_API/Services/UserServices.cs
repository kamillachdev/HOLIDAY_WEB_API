using HOLIDAY_WEB_API.data_access;
using HOLIDAY_WEB_API.Models;

namespace HOLIDAY_WEB_API.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserDbContext _dbCOntext;
        public UserServices(UserDbContext dbContext)
        {
            _dbCOntext = dbContext;
        }

        public User GetUserByEmail(string email, string password)
        {
            var user =  _dbCOntext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user;
        }
    }
}
