using HOLIDAY_WEB_API.Models;

namespace HOLIDAY_WEB_API.Interfaces
{
    public interface IAuthDL
    {
        public Task<BaseResponse> SignUp(SignUpRequest request);

        public Task<BaseResponse> SignIn(SignInRequest request);
    }
}
