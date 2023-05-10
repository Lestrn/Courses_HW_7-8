using Courses_HW_7_8.DB;
using Courses_HW_7_8.DB.Models;
using Courses_HW_7_8.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Courses_HW_7_8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AccountingDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IEntityRepository<CostCategories>, EntityRepository<CostCategories>>();
            builder.Services.AddScoped<IEntityRepository<CostFields>, EntityRepository<CostFields>>();
            builder.Services.AddControllersWithViews();                  
            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}