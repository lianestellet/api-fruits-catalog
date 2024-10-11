using Core.DTOs;
using Core.Interfaces;

namespace DataAccess.Repositories
{
    public class FruitRepository : IFruitRepository
    {
        private readonly IFruitContext _context;
        public FruitRepository(IFruitContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FruitDTO>> FindAllFruits()
        {
            return await _context.FindAllFruits();
        }

        public async Task DeleteFruit(long id)
        {
            await _context.FindById(id);
        }


        public Task<FruitDTO> FindById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<FruitDTO> Save(FruitDTO fruitDTO)
        {
            throw new NotImplementedException();
        }

        public Task<FruitDTO> Update(FruitDTO fruitDTO)
        {
            throw new NotImplementedException();
        }
    }
}
