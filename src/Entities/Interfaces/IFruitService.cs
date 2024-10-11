using Core.DTOs;

namespace Core.Interfaces
{
    public interface IFruitService
    {
        Task<IEnumerable<FruitDTO>> FindAllFruits();
        Task<FruitDTO> FindFruitById(long id);
        Task SaveFruit(FruitDTO fruit);
        Task UpdateFruit(FruitDTO fruitDto);
        Task DeleteFruit(long id);
    }
}
