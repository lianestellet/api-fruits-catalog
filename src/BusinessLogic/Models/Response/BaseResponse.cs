namespace BusinessLogic.Models.Response
{
    public class BaseResponse (string message)
    {
        public string? Message { get; set; } = message;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
