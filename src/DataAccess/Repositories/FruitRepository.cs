using DataAccess.Context;
using Entities.Domain;
using Entities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class FruitRepository(FruitDbContext context) : IFruitRepository
    {
        private readonly FruitDbContext _context = context;

        public async Task<IEnumerable<Fruit>> FindAllFruitsAsync()
        {
            return await _context.Fruits
                .Include(f => f.FruitType)
                .ToListAsync();
        }

        public async Task<Fruit> FindFruitByIdAsync(long fruitId)
        {
            var fruit = await _context.Fruits
                .Include(f => f.FruitType)
                .FirstOrDefaultAsync(f => f.Id == fruitId);

            return fruit ?? throw new FruitNotFoundException(fruitId);
        }

        public async Task<Fruit> SaveFruitAsync(Fruit fruit)
        {
            await _context.Fruits.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return fruit;
        }

        public async Task<Fruit> UpdateFruitAsync(Fruit fruit)
        {
            _context.Fruits.Update(fruit);            
            await _context.SaveChangesAsync();
            return fruit;
        }

        public async Task DeleteFruitAsync(long fruitId)
        {
            var fruit = await _context.Fruits.FindAsync(fruitId) ??
                throw new FruitNotFoundException(fruitId);

            _context.Fruits.Remove(fruit);
            await _context.SaveChangesAsync();
        }

        public async Task<FruitType> FindFruitTypeByIdAsync(long fruitTypeId)
        {
            var fruitType = await _context.FruitTypes.FirstOrDefaultAsync(ft => ft.Id == fruitTypeId);
            return fruitType ?? throw new FruitTypeNotFoundException(fruitTypeId);
        }
    }
}
