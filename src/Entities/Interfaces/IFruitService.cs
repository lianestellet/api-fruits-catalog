using Entities.DTOs;

namespace Entities.Interfaces
{
    public interface IFruitService
    {
        Task<IEnumerable<FruitDTO>> FindAllFruitsAsync();
        Task <FruitDTO> FindFruitByIdAsync(long id);
        Task <FruitDTO> SaveFruitAsync(CreateFruitDTO fruit);
        Task <FruitDTO> UpdateFruitAsync(FruitDTO fruitDto);
        Task DeleteFruitAsync(long id);
    }
}
