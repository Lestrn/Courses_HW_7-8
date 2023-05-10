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

    }
}
