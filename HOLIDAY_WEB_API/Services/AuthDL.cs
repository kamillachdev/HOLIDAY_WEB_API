using HOLIDAY_WEB_API.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace HOLIDAY_WEB_API.Services
{
    public class AuthDL : IAuthDL
    {
        private readonly SignUpInDbContext _context;

        public AuthDL(YourDbContext context)
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
                UserDetail user = await _context.UserDetails.FirstOrDefaultAsync(u =>
                    u.UserName == request.UserName && u.PassWord == request.Password && u.Role == request.Role);

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
                if (!request.Password.Equals(request.ConfigPassword))
                {
                    response.IsSuccess = false;
                    response.Message = "Password & Confirm Password not Match";
                    return response;
                }

                UserDetail user = new UserDetail
                {
                    UserName = request.UserName,
                    PassWord = request.Password,
                    Role = request.Role
                };

                _context.UserDetails.Add(user);
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
