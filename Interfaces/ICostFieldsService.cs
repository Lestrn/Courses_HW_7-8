using Courses_HW_7_8.DB.Enums;
using Courses_HW_7_8.DB.Models;

namespace Courses_HW_7_8.Interfaces
{
    public interface ICostFieldsService
    {
        public Task UpdateCategoryInField(CostCategories category, int costFieldid);
        public Task<decimal> CalculateCostByCategory(Categories category, TimePeriod period);
        public Task<decimal> CalculateCostByCategory(int categoryId, TimePeriod period);
        public Task<decimal> CalculateCostByCategory(Categories category, string dateFrom, string dateUntil);
        public Task<decimal> CalculateCostByCategory(int categoryId, string dateFrom, string dateUntil);
        public Task<decimal> CalculateTotalCost(TimePeriod period);
        public Task<decimal> CalculateTotalCost(string dateFrom, string dateUntil);
        public Task UpdateCostField(CostFields costFields);
        public Task DeleteCostField(int costFieldId);
        public Task AddCostField(CostFields costField);
        public Task<List<CostFields>> GetAllCostFields();
        public Task<CostFields?> GetCostFeildById(int costFieldId);
    }
}
