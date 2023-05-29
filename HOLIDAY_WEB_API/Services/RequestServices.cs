using HOLIDAY_WEB_API.data_access;
using HOLIDAY_WEB_API.Models;

namespace HOLIDAY_WEB_API.Services
{

    public class RequestServices : IRequestServices
    {
        private readonly UserDbContext _dbCOntext;
        public RequestServices(UserDbContext dbContext)
        {
            _dbCOntext = dbContext;
        }

        List<Request> IRequestServices.GetAllRequests()
        {
            var requests = _dbCOntext.Requests.ToList();
            return requests;
        }

        public void CreateRequest(Request request) 
        {
            if (request == null) 
            {
                throw new ArgumentNullException(nameof(request));
            }

            _dbCOntext.Requests.Add(request);
            _dbCOntext.SaveChanges();
        }
    }
}
