using System.Net; 

namespace Entities.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
        public int Status { get; } = (int)HttpStatusCode.NotFound;
        public string Msg { get; } = message;
        public DateTime Date { get; } = DateTime.UtcNow;

        public object ToFriendlyJson()
        {
            return new
            {
                status = Status,
                msg = Msg,
                date = Date.ToString("o")
            };
        }
    }
}
