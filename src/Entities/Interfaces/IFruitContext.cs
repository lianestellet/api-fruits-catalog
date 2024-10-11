using Core.DTOs;

namespace Core.Interfaces
{
    public interface IFruitContext
    {
        Task<IEnumerable<FruitDTO>> FindAllFruits();
        Task<FruitDTO> FindById(long id);
        Task<FruitDTO> Save(FruitDTO fruitDTO);
        Task<FruitDTO> Update(FruitDTO fruitDTO);
        Task DeleteFruit(long id);
    }
}
