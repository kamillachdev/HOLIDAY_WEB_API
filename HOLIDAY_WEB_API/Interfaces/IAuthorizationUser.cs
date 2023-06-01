namespace HOLIDAY_WEB_API.Interfaces
{
    public interface IAuthorizationUser
    {
        public string CreateJwt(string userId, string username, string email, string role);
        public void ClearJwtCookie();
        public int GetUserIdFromClaims();
    }
}
