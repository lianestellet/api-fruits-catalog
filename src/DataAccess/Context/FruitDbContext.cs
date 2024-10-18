using Entities.Domain;
using Entities.DTOs;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class FruitDbContext : DbContext
    {
        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<FruitType> FruitTypes { get; set; }

        public FruitDbContext(DbContextOptions<FruitDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fruit>()
                .HasOne(f => f.FruitType)
                .WithMany()
                .HasForeignKey(f => f.FruitTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Additional model configuration
        }
    }
}
