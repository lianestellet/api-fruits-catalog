using Entities.Domain;

namespace Entities.Interfaces
{
    public interface IFruitRepository
    {
        Task<IEnumerable<Fruit>> FindAllFruitsAsync();
        Task<Fruit?> FindFruitByIdAsync(long fruitId);
        Task<Fruit> SaveFruitAsync(Fruit fruit);
        Task<Fruit> UpdateFruitAsync(Fruit fruit);
        Task DeleteFruitAsync(Fruit fruit);
    }
}
