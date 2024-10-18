using Entities.DTOs;

namespace Entities.Interfaces
{
    public interface IFruitService
    {
        Task<IEnumerable<FruitDTO>> FindAllFruits();
        Task <FruitDTO> FindFruitById(long id);
        Task <FruitDTO> SaveFruit(FruitDTO fruit);
        Task <FruitDTO> UpdateFruit(FruitDTO fruitDto);
        Task DeleteFruit(long id);
    }
}
