using Entities.Constants;

namespace Entities.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
        public int Status { get; } = AppConstants.StatusCodes.NOT_FOUND;
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
