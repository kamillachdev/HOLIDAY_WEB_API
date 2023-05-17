namespace HOLIDAY_WEB_API.Models
{
    public class Request
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int UserId { get; set; }
    }
}
