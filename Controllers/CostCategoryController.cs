using Courses_HW_7_8.DB.Models;
using Courses_HW_7_8.Interfaces;
using Courses_HW_7_8.Models;
using Microsoft.AspNetCore.Mvc;

namespace Courses_HW_7_8.Controllers
{
    public class CostCategoryController : Controller
    {
        private ICostCategoriesService _costCategoriesService;
        public CostCategoryController(ICostCategoriesService costCategoriesService)
        {
            _costCategoriesService = costCategoriesService;
        }
        public async Task<IActionResult> Category()
        {
            var categories = await _costCategoriesService.GetAllCategories();
            return View(categories);
        }
        public async Task<IActionResult> AddCategory(string categoryName)
        {
            try
            {
                await _costCategoriesService.AddCategory(new CostCategories() { Name = categoryName });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Category", "CostCategory");
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _costCategoriesService.DeleteCategory(id);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Category", "CostCategory");
        }
        public async Task<IActionResult> EditCategory(int id, string name)
        {
            CostCategories category = await _costCategoriesService.GetCategoryById(id);
            category.Name = name;
            try
            {
                await _costCategoriesService.UpdateCategory(category);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Category", "CostCategory");
        }
    }
}
