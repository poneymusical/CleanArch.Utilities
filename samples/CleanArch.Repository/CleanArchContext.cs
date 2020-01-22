using CleanArch.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Repository
{
    public class CleanArchContext : DbContext
    {
        public CleanArchContext(DbContextOptions<CleanArchContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var myEntity = modelBuilder.Entity<MyEntity>().HasKey(m => m.Id);
        }
    }
}