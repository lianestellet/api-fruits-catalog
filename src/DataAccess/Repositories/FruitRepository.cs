using DataAccess.Context;
using Entities.Domain;
using Entities.Exceptions;
using Entities.Interfaces;
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

        public async Task<Fruit?> FindFruitByIdAsync(long fruitId)
        {
            return await _context.Fruits
                .Include(f => f.FruitType)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == fruitId);
        }

        public async Task<Fruit> SaveFruitAsync(Fruit fruit)
        {
            await _context.Fruits.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return fruit;
        }

        public async Task<Fruit> UpdateFruitAsync(Fruit updateFruitData)
        {
            var fruitToUpdate = await _context.Fruits.FindAsync(updateFruitData.Id) ?? 
                throw new NotFoundException(ExceptionMessages.FruitNotFoundById(updateFruitData.Id));

            fruitToUpdate.Name = updateFruitData.Name;
            fruitToUpdate.Description = updateFruitData.Description;
            fruitToUpdate.FruitTypeId = updateFruitData.FruitTypeId;

            _context.Fruits.Update(fruitToUpdate);
            await _context.SaveChangesAsync();
            return updateFruitData;
        }

        public async Task DeleteFruitAsync(long fruitId)
        {
            var fruit = await _context.Fruits.FindAsync(fruitId) ??
                throw new NotFoundException(ExceptionMessages.FruitNotFoundById(fruitId));

            _context.Fruits.Remove(fruit);
            await _context.SaveChangesAsync();
        }

        public async Task<FruitType?> FruitTypeByIdAsync(long fruitTypeId)
        {
            return await _context.FruitTypes.FirstOrDefaultAsync(ft => ft.Id == fruitTypeId);
        }
    }
}
