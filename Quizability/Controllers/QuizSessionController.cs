using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizability.Models;
using Quizability.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quizability.Controllers
{
    [Authorize]
    public class QuizSessionController : Controller
    {
        Quizability_serverContext db;
        public QuizSessionController(Quizability_serverContext db)
        {
            this.db = db;
        }
       
        [Route("QuizSession/{quizId?}/{questionId?}")]
        [HttpGet]
        public IActionResult Session(int quizId,int questionId)
        {
            QuestionsAnswersViewModel questionsAnswers = new QuestionsAnswersViewModel { };
            questionsAnswers.Questions = db.Questions.Where(q => q.QuizId == quizId).ToList();
            foreach (Question question in questionsAnswers.Questions)
            {
                question.Answers = db.Answers.Where(a => a.QuestionId == question.QuestionId).ToList();
            }
            
            TempData["quizId"] = quizId;
            if (questionId == 0) {
                
                TempData["questionId"] = questionsAnswers.Questions.Select(q => q.QuestionId).FirstOrDefault();

                string userEmail = this.User.Identity.Name;
                User user = db.Users.FirstOrDefault(u=>u.Email== userEmail);

                TimeSpan? ts = db.Quizes.Where(q => q.QuizId == quizId).Select(q => q.QuizTime).FirstOrDefault();
                DateTime finishTime = DateTime.Now + (TimeSpan)ts;
                //Записать финиш тайм тоже в бд
                TempData["finishTime"] = finishTime;

                db.UserQuizzes.Add(new UserQuiz { UserId = user.UserId, QuizId = quizId, StartTme = System.DateTime.Now, Points=0, FinishTime = finishTime,RightAnswersAmount=0, Finished=false});
                db.SaveChanges();

                return View(questionsAnswers.Questions.First());
            }
            
                return View(questionsAnswers.Questions.FirstOrDefault(q=>q.QuestionId==questionId));
            
            
            
            
            
        }

        [Route("QuizSession/{chosenAnswer?}")]
        [Route("QuizSession/{quizId?}/{questionId}")]
        [HttpPost]
        public IActionResult Session(int chosenAnswer)
        {
            QuestionsAnswersViewModel questionsAnswers = new QuestionsAnswersViewModel { };
            questionsAnswers.Questions = db.Questions.Where(q => q.QuizId == (int)TempData.Peek("quizId")).ToList();
            foreach (Question q in questionsAnswers.Questions)
            {
                q.Answers = db.Answers.Where(a => a.QuestionId == q.QuestionId).ToList();
            }
            
            string userEmail = this.User.Identity.Name;
            User user = db.Users.FirstOrDefault(u => u.Email == userEmail);

            Question question = db.Questions.FirstOrDefault(q=>q.QuestionId==(int)TempData.Peek("quizId"));
            Answer rightAnswer = db.Answers.FirstOrDefault(a => (a.Correct == true) && (a.QuestionId == (int)TempData.Peek("questionId")));

            UserQuiz userSession = (from uq in db.UserQuizzes
                                    where (uq.UserId == user.UserId) && (uq.QuizId == (int)TempData.Peek("quizId"))
                                    select uq).OrderBy(uq=>uq.StartTme).LastOrDefault();

            if (chosenAnswer == rightAnswer.AnswerId)
            {
                
                userSession.Points += question.QuestionPoints;
                userSession.RightAnswersAmount += 1;
                db.SaveChanges();

            }

            
              int nextQuestionId =questionsAnswers.Questions.SkipWhile(q => q.QuestionId != (int)TempData["questionId"])
                .Skip(1).Select(q=>q.QuestionId).FirstOrDefault();

            if (nextQuestionId != 0)
            {
                TempData["questionId"] = nextQuestionId;
                return RedirectToAction("Session", "QuizSession", new
                {
                    quizId = TempData.Peek("quizId"),
                    questionId = nextQuestionId
                });
            }
            else
            {
                userSession.FinishTime = System.DateTime.Now;
                userSession.Finished = true;
                db.SaveChanges();
                TempData["time"] = userSession.FinishTime;
                //Реализовать здесь обработку результата пользователя c учетом времени

                return RedirectToAction("ShowQuizResults","QuizSession", new {
                quizId=TempData["quizId"]
                });
            }

        }
        [Route("QuizResults/{quizId}")]
        public IActionResult ShowQuizResults(int quizId)
        {
            
            string userEmail = this.User.Identity.Name;
            User user = db.Users.FirstOrDefault(u => u.Email == userEmail);
            DateTime? time = (DateTime?)TempData["time"];
            if (time != null)
            {
                Achievement forUser = db.Achievements.Where(a => a.QuizId == quizId).First();
                int check = db.UserAchievements.Where(ua => ua.AchievementId == forUser.AchievementId && ua.UserId == user.UserId).Count();
                if (check == 0)
                {
                    UserAchievement testAch = new UserAchievement()
                    {
                        AchievementId = forUser.AchievementId,
                        UserId = user.UserId,
                        ObtainingTime = time,
                        Achievement = forUser,
                        User = user
                    };
                    db.UserAchievements.Add(testAch);
                    db.SaveChanges();
                }
            }
            Quize quiz = db.Quizes.FirstOrDefault(q=>q.QuizId==quizId);

            ViewBag.UserId=user.UserId;
            ViewBag.QuizId = quizId;
            ViewBag.QuestionsAmount = db.Questions.Where(q => q.QuizId == quizId).Count();


            List<UserQuiz> usersQuizes = db.UserQuizzes.ToList();
            foreach(var uq in usersQuizes)
            {
                uq.Quiz = db.Quizes.FirstOrDefault(q => q.QuizId == uq.QuizId);
                uq.User = db.Users.FirstOrDefault(u => u.UserId == uq.UserId);
            }
            ViewBag.Data = ViewRating();
            
            return View(usersQuizes);
        }

        
            
            private List<User> GetUserInfo()
            {
                List<User> info = new List<User>();
                List<User> dbInfo =db.Users.ToList();
                if (dbInfo?.Any() == true)
                {
                    foreach (var u in dbInfo)
                    {
                        info.Add(new User()
                        {
                            UserId = u.UserId,
                            Name = u.Name,
                            Email = u.Email,
                            Password = u.Password,
                            StatusId = u.StatusId
                        });
                    }
                }
                return info;
            }
            private List<UserQuiz> ViewRating()
            {
                List<UserQuiz> ratingData =  db.UserQuizzes.Where(uq => uq.Finished == true).ToList();
                List<UserQuiz> data = new List<UserQuiz>();
                List<User> userList = GetUserInfo();
                if (ratingData?.Any() == true)
                {
                    foreach (var uq in ratingData)
                    {
                        data.Add(new UserQuiz()
                        {
                            UserId = uq.UserId,
                            QuizId = uq.QuizId,
                            StartTme = uq.StartTme,
                            FinishTime = uq.FinishTime,
                            Points = uq.Points,
                            RightAnswersAmount = uq.RightAnswersAmount,
                            Finished = uq.Finished,
                            User = userList.Where(u => u.UserId == uq.UserId).First()
                        });
                    }
                }
            return data;
                
            }
        

    }
}
