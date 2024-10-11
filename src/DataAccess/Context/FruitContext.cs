using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class FruitContext : DbContext, IFruitContext
    {
        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<FruitType> FruitTypes { get; set; }

        public FruitContext(DbContextOptions<FruitContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional model configuration
        }

        public async Task<IEnumerable<FruitDTO>> FindAllFruits() => throw new NotImplementedException();

        public Task<FruitDTO> FindById(long id) => throw new NotImplementedException();

        public Task<FruitDTO> Save(FruitDTO fruitDTO) => throw new NotImplementedException();

        public Task<FruitDTO> Update(FruitDTO fruitDTO) => throw new NotImplementedException();

        public Task DeleteFruit(long id) => throw new NotImplementedException();
    }
}
