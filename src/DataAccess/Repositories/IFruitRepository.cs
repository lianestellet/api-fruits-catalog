using Entities.Domain;

namespace DataAccess.Repositories
{
    public interface IFruitRepository
    {
        Task<IEnumerable<Fruit>> FindAllFruitsAsync();
        Task<Fruit> FindFruitByIdAsync(long fruitId);
        Task<Fruit> SaveFruitAsync(Fruit fruit);
        Task<Fruit> UpdateFruitAsync(Fruit fruit);
        Task DeleteFruitAsync(long fruitId);
        Task<FruitType> FindFruitTypeByIdAsync(long fruitTypeId);
    }
}
