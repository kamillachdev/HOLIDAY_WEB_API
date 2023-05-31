using HOLIDAY_WEB_API.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using HOLIDAY_WEB_API.data_access;
using HOLIDAY_WEB_API.Interfaces;

namespace HOLIDAY_WEB_API.Services
{
    public class AuthDL : IAuthDL
    {
        private readonly UserDbContext _context;

        public AuthDL(UserDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse> SignIn(SignInRequest request)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u =>
                    u.UserName == request.UserName && u.Email == request.Email && u.Password == request.Password && u.Role == request.Role);


            if (user == null)
            {
                return new BaseResponse("Login Unsuccessfully");
            }

            var response = new BaseResponse();

            return response;
        }

        public async Task<BaseResponse> SignUp(SignUpRequest request)
        {
            User user = new User
            {
                 UserName = request.UserName,
                 Password = request.Password,
                 Role = request.Role,
                 Email = request.Email
            };

            _context.Users.Add(user);
            int status = await _context.SaveChangesAsync();

            if (status <= 0)
            {
                 return new BaseResponse("Something Went Wrong");
            }
            BaseResponse response = new BaseResponse();

            return response;
        }
    }
}
