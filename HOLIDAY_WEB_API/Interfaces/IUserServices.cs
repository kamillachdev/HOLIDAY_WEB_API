using HOLIDAY_WEB_API.Models;

namespace HOLIDAY_WEB_API.Interfaces
{
    public interface IUserServices
    {
        public User GetUserByEmail(string email, string password);
    }
}
