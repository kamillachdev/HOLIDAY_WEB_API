using HOLIDAY_WEB_API.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using HOLIDAY_WEB_API.data_access;

namespace HOLIDAY_WEB_API.Services
{
    public class AuthDL : IAuthDL
    {
        private readonly UserDbContext _context;

        public AuthDL(UserDbContext context)
        {
            _context = context;
        }

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u =>
                    u.UserName == request.UserName && u.Password == request.Password && u.Role == request.Role);
                if (user != null)
                {
                    response.Message = "Login Successfully";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Login Unsuccessfully";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    response.IsSuccess = false;
                    response.Message = "Password & Confirm Password not Match";
                    return response;
                }

                User user = new User
                {
                    UserName = request.UserName,
                    Password = request.Password,
                    Role = request.Role
                };

                _context.Users.Add(user);
                int status = await _context.SaveChangesAsync();

                if (status <= 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Something Went Wrong";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
