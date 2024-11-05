namespace Entities.Exceptions
{
    public static class ExceptionMessages
    {
        public static string FruitNotFoundById(long fruitId) => $"Fruit with Id: {fruitId} was not found.";
        public static string FruitTypeNotFoundById(long fruitTypeId) => $"FruitType with Id: {fruitTypeId} was not found.";
    }
}
