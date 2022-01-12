using LoanCalculatorMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoanCalculatorMVC.Helper;

namespace LoanCalculatorMVC.Controllers
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

        [HttpGet]
        public IActionResult LCPage()
        {
            Loan model = new();
             
            model.Amount = 15000m;
            model.Rate = 3.5m;
            model.TotalInterest = 0.0m;
            model.TotalCost = 0.0m;
            model.Payment = 0.0m;
            model.Term = 60;

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult LCPage(Loan loan)
        {
            var loanHelper = new LoanHelper();
            Loan newloan = loanHelper.GetPayments(loan);
            return View(newloan);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
