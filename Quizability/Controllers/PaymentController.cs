using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quizability.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public PaymentController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // GET: PaymentController
        public IActionResult PremiumPayment()
        {
            return View();
        }
        public IActionResult PremiumPayment2()
        {
            return View();
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
