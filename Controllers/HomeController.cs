using Courses_HW_7_8.DB.Models;
using Courses_HW_7_8.Interfaces;
using Courses_HW_7_8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Courses_HW_7_8.Controllers
{
    public class HomeController : Controller
    {
        private ICostFieldsService _costFieldsService;
        private ICostCategoriesService _costCategoriesService;
        private IEntityRepository<CostCategories> _costCategoryRepository;

        public HomeController(ICostFieldsService  costFieldsService, ICostCategoriesService costCategoriesService, IEntityRepository<CostCategories> costCategoryRepository)
        {
            _costFieldsService = costFieldsService;
            _costCategoriesService = costCategoriesService;
            _costCategoryRepository = costCategoryRepository;
        }  
        public async Task<IActionResult> Index()
        {
            var categores = await _costCategoriesService.GetAllCategories();
            return View(new HomeModel(categores.ToList()));
        }
        public async Task<IActionResult> Error()
        {
            return View(new ErrorModel { Message = TempData["ErrorMessage"].ToString() });
        }
        public async Task<IActionResult> CalculateMonthlyCosts(int categoryId)
        {
            decimal result = 0;
            try
            {
                 result = await _costFieldsService.CalculateCostByCategory(categoryId, DB.Enums.TimePeriod.OneMonth);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            var categores = await _costCategoriesService.GetAllCategories();
            return View("Index", new HomeModel(categores.ToList()) { Result = result });
        }
        public async Task<IActionResult> CalculateCustomDateCosts(int categoryId, string dateFrom, string dateUntil)
        {
            decimal result = 0;
            try
            {
                result = await _costFieldsService.CalculateCostByCategory(categoryId, dateFrom, dateUntil);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            var categores = await _costCategoriesService.GetAllCategories();
            return View("Index", new HomeModel(categores.ToList()) { Result = result });
        }

    }
}