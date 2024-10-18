namespace Entities.Exceptions
{
    public class NotFoundException : Exception
    {
        public int Status { get; }
        public DateTime Date { get; }

        public NotFoundException(string message) : base(message) {
            Status = 404;
            Date = DateTime.UtcNow;
        }
    }
}
