using Entities.DTOs;

namespace Entities.Interfaces
{
    public interface IFruitService
    {
        Task<IEnumerable<FruitDTO>> FindAllFruits();
        Task <FruitDTO> FindFruitByIdAsync(long id);
        Task <FruitDTO> SaveFruitAsync(CreateFruitDTO fruit);
        Task <FruitDTO> UpdateFruitAsync(FruitDTO fruitDto);
        Task DeleteFruit(long id);
    }
}
