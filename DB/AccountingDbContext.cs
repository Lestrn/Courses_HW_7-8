using Courses_HW_7_8.DB.Enums;
using Courses_HW_7_8.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses_HW_7_8.DB
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<CostCategories> CostCategories { get; set; }
        public DbSet<CostFields> CostFields { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CostCategories>()
                .HasData(
                    new CostCategories { Id = 1, Name = Categories.Entertainment.ToString() },
                    new CostCategories { Id = 2, Name = Categories.Internet.ToString() },
                    new CostCategories { Id = 3, Name = Categories.Transport.ToString() },
                    new CostCategories { Id = 4, Name = Categories.MobileConnection.ToString() },
                    new CostCategories { Id = 5, Name = Categories.Food.ToString() }
                );
            base.OnModelCreating(modelBuilder);
        }

    }
}
