﻿using Courses_HW_7_8.DB.Models;
using Courses_HW_7_8.Interfaces;
using Courses_HW_7_8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.RegularExpressions;

namespace Courses_HW_7_8.Controllers
{
    public class CostFeildController : Controller
    {
        private IEntityRepository<CostCategories> _costCategoryRepository;
        private ICostFieldsService _costFieldsService;
        public CostFeildController(IEntityRepository<CostCategories> costCategoryRepository, ICostFieldsService costFieldsService)
        {
            _costCategoryRepository = costCategoryRepository;
            _costFieldsService = costFieldsService;
        }
        public async Task<IActionResult> Feild()
        {
            var feilds = await _costFieldsService.GetAllCostFields();
            var categories = await _costCategoryRepository.GetAllAsync();
            return View(new FeildModel(categories, feilds));
        }
        public async Task<IActionResult> AddFeild(string category, string cost, DateTime date, string description)
        {
            cost = Regex.Replace(cost, @"\.", ",");
            decimal costConverted = decimal.Parse(cost);
            var costCategory = await _costCategoryRepository.Where(ctg => category == ctg.Name);
            if(costCategory == null)
            {
                TempData["ErrorMessage"] = "Couldnt find that category";
                return RedirectToAction("Error", "Home");
            }
            await _costFieldsService.AddCostField(new CostFields() { Category = costCategory.First(), Cost = costConverted, Date = date, Description = description });
            return RedirectToAction("Feild", "CostFeild");
        }
        public async Task<IActionResult> DeleteFeild(int id)
        {
            try
            {
                await _costFieldsService.DeleteCostField(id);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Feild", "CostFeild");
        }
        public async Task<IActionResult> EditFeild(int id, string date, string description,  string category, string cost)
        {
            cost = Regex.Replace(cost, @"\.", ",");
            decimal costConverted = decimal.Parse(cost);
            var costCategory = await _costCategoryRepository.Where(ctg => category == ctg.Name);
            if (costCategory == null)
            {
                return RedirectToAction("Error", "Home", "Couldnt find category in db");
            }
            CostFields feild = await _costFieldsService.GetCostFeildById(id);
            if(feild == null)
            {
                TempData["ErrorMessage"] = "Couldnt find feild with this id";
                return RedirectToAction("Error", "Home");
            }
            bool dateIsParsed = DateTime.TryParse(date, out DateTime parsedDateTime);
            feild.Date = parsedDateTime;
            feild.Description = description;
            feild.Category = costCategory.First();
            feild.Cost = costConverted;
            try
            {
                await _costFieldsService.UpdateCostField(feild);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Feild", "CostFeild");
        }
    }
}
