using HOLIDAY_WEB_API.Models;

namespace HOLIDAY_WEB_API.Interfaces
{
    public interface IRequestServices
    {
        public List<Request> GetAllRequests();
        void CreateRequest(Request request);
    }
}
