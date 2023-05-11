using Courses_HW_7_8.DB.Enums;
using Courses_HW_7_8.DB.Models;
using Courses_HW_7_8.Interfaces;
using System.Data;

namespace Courses_HW_7_8.Services
{
    public class CostFieldService : ICostFieldsService
    {
        private readonly IEntityRepository<CostFields> _costFieldRepository;
        private readonly IEntityRepository<CostCategories> _costCategoryRepository;
        public CostFieldService(IEntityRepository<CostFields> costFieldsRepository, IEntityRepository<CostCategories> costCategoryRepository)
        {
            _costFieldRepository = costFieldsRepository;
            _costCategoryRepository = costCategoryRepository;
        }

        public async Task AddCostField(CostFields costField)
        {
            if (costField == null || costField.Category == null)
            {
                throw new ArgumentNullException("Cost field or category in it was null");
            }
            await _costFieldRepository.AddAsync(costField);
            await _costFieldRepository.SaveChangesAsync();
        }

        public async Task<decimal> CalculateCostByCategory(Categories category, TimePeriod period)
        {
            CostCategories categoryFromDb = await _costCategoryRepository.FindByIdAsync((int)category);
            if(categoryFromDb == null)
            {
                throw new ArgumentException("Default category was deleted, try to use method by id");
            }
            DateTime dateNow = DateTime.Now;
            DateTime dateTimePeriodAgo = dateNow.AddMonths(-(int)period);
            return await GeneralCostByCategoryCalculator(category.ToString(), dateTimePeriodAgo, dateNow);         
        }

        public async Task<decimal> CalculateCostByCategory(int categoryId, TimePeriod period)
        {
            CostCategories category = await _costCategoryRepository.FindByIdAsync(categoryId);
            DateTime dateNow = DateTime.Now;
            DateTime dateTimePeriodAgo = dateNow.AddMonths(-(int)period);
            return await GeneralCostByCategoryCalculator(category.Name, dateTimePeriodAgo, dateNow);      
        }

        public async Task<decimal> CalculateCostByCategory(Categories category, DateOnly dateFrom, DateOnly dateUntil)
        {
            CostCategories categoryFromDb = await _costCategoryRepository.FindByIdAsync((int)category);
            if (categoryFromDb == null)
            {
                throw new ArgumentException("Default category was deleted, try to use method by id");
            }
            return await GeneralCostByCategoryCalculator(category.ToString(), DateTime.Parse(dateFrom.ToString()), DateTime.Parse(dateUntil.ToString()));        
        }

        public async Task<decimal> CalculateCostByCategory(int categoryId, DateOnly dateFrom, DateOnly dateUntil)
        {
            CostCategories category = await _costCategoryRepository.FindByIdAsync(categoryId);
            return await GeneralCostByCategoryCalculator(category.Name, DateTime.Parse(dateFrom.ToString()), DateTime.Parse(dateUntil.ToString()));           
        }

        public async Task<decimal> CalculateTotalCost(TimePeriod period)
        {
            DateTime dateNow = DateTime.Now;
            DateTime dateTimePeriodAgo = dateNow.AddMonths(-(int)period);
            return await GeneralTotalCostCalculator(dateTimePeriodAgo, dateNow);
        }

        public async Task<decimal> CalculateTotalCost(DateOnly dateFrom, DateOnly dateUntil)
        {
            return await (GeneralTotalCostCalculator(DateTime.Parse(dateFrom.ToString()), DateTime.Parse(dateUntil.ToString())));
        }

        public async Task RemoveCostField(int costFieldId)
        {
            CostFields costField = await _costFieldRepository.FindByIdAsync(costFieldId);
            if (costField == null)
            {
                throw new ArgumentException("Cost field wasnt found with this id");
            }
            await _costFieldRepository.DeleteAsync(costField);
            await _costFieldRepository.SaveChangesAsync();
        }

        public async Task UpdateCategoryInField(CostCategories category, int costFieldid)
        {
            CostCategories costCategory = await _costCategoryRepository.FindByIdAsync(category.Id);
            CostFields costField = await _costFieldRepository.FindByIdAsync(costFieldid);
            if (costCategory == null || costField == null)
            {
                throw new ArgumentException("Cost field or category wasnt found with this id");
            }
            costField.Category = costCategory;
            await _costFieldRepository.UpdateAsync(costField);
            await _costFieldRepository.SaveChangesAsync();
        }
        public async Task UpdateCostField(CostFields costFields)
        {
            CostFields costField = await _costFieldRepository.FindByIdAsync(costFields.Id);
            if (costField == null || costField.Category == null)
            {
                throw new ArgumentException("Cost field wasnt found with this id");
            }
            costField.Description =  costFields.Description;
            costField.Cost = costFields.Cost;
            costField.Category = costFields.Category;
            costField.Date = costFields.Date;
            await _costFieldRepository.UpdateAsync(costField);
            _costFieldRepository.SaveChangesAsync();
        }
        private async Task<decimal> GeneralCostByCategoryCalculator(string category, DateTime fromDate , DateTime untilDate)
        {
            var costFields = await _costFieldRepository.Where(costField => costField.Category.Name == category && costField.Date <= untilDate && costField.Date >= fromDate);
            decimal totalSum = costFields.Sum(costField => costField.Cost);
            return totalSum;
        }
        private async Task<decimal> GeneralTotalCostCalculator(DateTime fromDate, DateTime untilDate)
        {
            var costFields = await _costFieldRepository.Where(costField => costField.Date <= untilDate && costField.Date >= fromDate);
            decimal totalSum = costFields.Sum(costField => costField.Cost);
            return totalSum;
        }
    }
}
