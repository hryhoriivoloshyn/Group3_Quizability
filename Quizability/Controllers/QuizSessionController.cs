using Microsoft.AspNetCore.Mvc;
using Quizability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.Controllers
{
    public class QuizSessionController : Controller
    {
        Quizability_serverContext db;
        public QuizSessionController(Quizability_serverContext db)
        {
            this.db = db;
        }

        public IActionResult StartQuizSession(int quizId)
        {
            List<Question> quizQuiestions = db.Questions.Where(q => q.QuizId == quizId).ToList();

            return View();
        }

        public async void StartTimer()
        {

        }
    }
}
