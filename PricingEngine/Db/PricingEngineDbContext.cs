using Microsoft.EntityFrameworkCore;
using PricingEngine.Db.Entities;

namespace PricingEngine.Db
{
    public class PricingEngineDbContext : DbContext
    {
        public DbSet<DbInputEntity> DbInputs { get; set; }
        public DbSet<UserInputEntity> UserInputs { get; set; }
        public DbSet<CalculatedInputEntity> CalculatedUserInputs { get; set; }




        public PricingEngineDbContext(DbContextOptions<PricingEngineDbContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DbInputEntity>()
                .HasData(new DbInputEntity());

            modelBuilder.Entity<UserInputEntity>()
                .HasData(new UserInputEntity());

        }
    }
}
