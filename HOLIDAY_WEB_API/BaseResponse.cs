namespace HOLIDAY_WEB_API
{
    public class BaseResponse

    {
        public List<string> Errors { get; set; } = new List<string>();

        public bool Success() { 
            return !Errors.Any();
        }

        public BaseResponse() { 
        }

        public BaseResponse(string error)
        {
            Errors.Add(error);
        }

        public BaseResponse(Exception exception)
        {
            Errors.Add(exception.Message);
        }
    }

}
