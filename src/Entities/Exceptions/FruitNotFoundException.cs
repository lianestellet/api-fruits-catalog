namespace Entities.Exceptions
{
    public class FruitNotFoundException(long fruitId) : Exception(ExceptionMessages.FruitNotFoundById(fruitId))
    {
        public long FruitId { get; } = fruitId;
        public string CustomMessage { get; } = ExceptionMessages.FruitNotFoundById(fruitId);
    }
}
