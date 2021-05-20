﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quizability.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.Controllers
{
    public class ForgetController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ForgetController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult ForgetIndex()
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