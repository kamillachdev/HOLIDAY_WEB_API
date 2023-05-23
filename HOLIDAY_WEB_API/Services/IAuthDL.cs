using HOLIDAY_WEB_API.Models;

namespace HOLIDAY_WEB_API.Services
{
    public interface IAuthDL
    {
        public Task<SignUpResponse> SignUp(SignUpRequest request);

        public Task<SignInResponse> SignIn(SignInRequest request);
    }
}
