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
    public class LogInController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public LogInController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult LogInIndex()
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
