using Courses_HW_7_8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Courses_HW_7_8.Controllers
{
    public class AccountingController : Controller
    {
        private readonly ILogger<AccountingController> _logger;

        public AccountingController(ILogger<AccountingController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}