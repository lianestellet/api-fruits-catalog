using Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class FruitDbContext : DbContext
    {
        public FruitDbContext(DbContextOptions<FruitDbContext> options) : base(options) { }

        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<FruitType> FruitTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fruit>()
                .HasOne(f => f.FruitType)
                .WithMany()
                .HasForeignKey(f => f.FruitTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
