using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.Controllers
{
    [Authorize]
    public class UserPageController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
