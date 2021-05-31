using Microsoft.AspNetCore.Mvc;
using Quizability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.Controllers
{
    public class QuizController : Controller
    {
        private Quizability_serverContext db;
        public QuizController(Quizability_serverContext db)
        {
            this.db = db;
        }
        public IActionResult Details(int id)
        {
            
                if (id == 0)
                {
                return Redirect("/Home/Error");
                }
                else
                {
                    Quize quiz = db.Quizes.FirstOrDefault(q => q.QuizId == id);
                    double? maxPoints = db.Questions.Where(question => question.QuizId == id).Sum(question=>question.QuestionPoints);
                    ViewBag.MaxPoints = maxPoints;
                    return View(quiz);
                }
        }

        public IActionResult PlayQuiz(int id)
        {
            
            return RedirectToAction("StartQuizSession", "QuizSession", new { quizId = id });
        }
    }
}
