using Azure.Core.Pipeline;
using Courses_HW_7_8.DB.Models;

namespace Courses_HW_7_8.Interfaces
{
    public interface ICostCategoriesService
    {
        public Task DeleteCategory(int categoryId);
        public Task AddCategory(CostCategories category);
        public Task UpdateCategory(CostCategories category);
        public Task<List<CostCategories>> GetAllCategories();
        public Task<CostCategories?> GetCategoryById(int categoryId);
    }
}
