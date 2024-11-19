using Entities.Domain;

namespace Entities.Exceptions
{
    public class FruitTypeNotFoundException(long fruitTypeId) : Exception(ExceptionMessages.FruitTypeNotFoundById(fruitTypeId))
    {
        public long FruitTypeId { get; } = fruitTypeId;
        public string CustomMessage { get; } = ExceptionMessages.FruitTypeNotFoundById(fruitTypeId);
    }
}
