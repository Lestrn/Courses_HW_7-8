using Courses_HW_7_8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Courses_HW_7_8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorModel { Message = TempData["ErrorMessage"].ToString() });
        }

    }
}