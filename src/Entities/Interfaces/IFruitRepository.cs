using Entities.DTOs;

namespace Entities.Interfaces
{
    public interface IFruitRepository
    {
        Task<IEnumerable<FruitDTO>> FindAllFruits();
        Task<FruitDTO> FindById(long id);
        Task<FruitDTO> Save(FruitDTO fruitDTO);
        Task<FruitDTO> Update(FruitDTO fruitDTO);
        Task DeleteFruit(long id);
    }
}
