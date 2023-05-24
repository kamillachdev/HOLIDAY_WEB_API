namespace HOLIDAY_WEB_API
{
    public class BaseResponse

    {
        public List<string> Errors { get; set; }

        public bool Success { get; set; }

        public BaseResponse() { 
            Success = true;
            Errors = new List<string>();
        }

        public BaseResponse(string error)
        {
            Success = false;
            Errors = new List<string>();

            Errors.Add(error);
        }

    }
}
