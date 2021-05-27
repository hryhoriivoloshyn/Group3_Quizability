using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.Controllers
{
    public class UserPageController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
