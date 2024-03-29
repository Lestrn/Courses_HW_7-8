﻿using Courses_HW_7_8.DB.Models;
using Courses_HW_7_8.Interfaces;

namespace Courses_HW_7_8.Services
{
    public class CostCategoryService : ICostCategoriesService
    {
        private readonly IEntityRepository<CostCategories> _categoryRepository;
        public CostCategoryService(IEntityRepository<CostCategories> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task AddCategory(CostCategories category)
        {
            if(category == null || string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("Category was null or field name was empty");
            }
           if(await _categoryRepository.Any(ctg => ctg.Name == category.Name))
            {
                throw new ArgumentException("Cant add already an existing category");
            }
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }
        public async Task DeleteCategory(int categoryId)
        {
            CostCategories category = await _categoryRepository.FindByIdAsync(categoryId);
            if(category == null)
            {
                throw new ArgumentException("Category with this id wasnt found");
            }
            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

  

        public async Task<List<CostCategories>> GetAllCategories()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<CostCategories?> GetCategoryById(int categoryId)
        {
            return await _categoryRepository.FindByIdAsync(categoryId);
        }

        public async Task UpdateCategory(CostCategories category)
        {
            if(category == null)
            {
                throw new ArgumentException("Category was null");
            }
            CostCategories categoryFound = await _categoryRepository.FindByIdAsync(category.Id);
            if (categoryFound == null)
            {
                throw new ArgumentException("Category with this id wasnt found");
            }
            var categoryWithTheSameName = _categoryRepository.Context.CostCategories.Any(ctg => ctg.Name == category.Name);    
            if (categoryWithTheSameName)
            {
                throw new ArgumentException("Cant edit category with already an existing name");
            }
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
