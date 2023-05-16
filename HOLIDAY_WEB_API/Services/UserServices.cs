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

        List<User> IUserServices.GetAllUsers()
        {
            var users =  _dbCOntext.Users.ToList();
            return users;
        }
    }
}
