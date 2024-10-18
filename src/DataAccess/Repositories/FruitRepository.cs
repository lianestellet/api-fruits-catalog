using DataAccess.Context;
using Entities.DTOs;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class FruitRepository : IFruitRepository
    {
        private readonly FruitDbContext _context;
        public FruitRepository(FruitDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FruitDTO>> FindAllFruits()
        {
            return await _context.Fruits
                .Include(f => f.FruitType)
                .Select(f => new FruitDTO
                {
                    Id = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    FruitType = new FruitTypeDTO
                    {
                        Id = f.FruitType.Id,
                        Name = f.FruitType.Name,
                        Description = f.FruitType.Description
                    }
                }).ToListAsync();
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
        public async Task DeleteFruit(long id)
        {
            throw new NotImplementedException();
        }

    }
}
