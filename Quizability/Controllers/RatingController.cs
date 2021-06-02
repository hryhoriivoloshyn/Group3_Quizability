using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.ViewModels
{
    public class RatingController : Controller
    {
        Quizability_serverContext db;
        public RatingController(Quizability_serverContext db)
        {
            this.db = db;
        }
        private async Task<List<User>> GetUserInfo()
        {
            List<User> info = new List<User>();
            List<User> dbInfo = await db.Users.ToListAsync();
            if(dbInfo?.Any()==true)
            {
                foreach(var u in dbInfo)
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
        public async Task<ViewResult> ViewRating()
        {
            List<UserQuiz> ratingData = await db.UserQuizzes.Where(uq => uq.Finished == true).ToListAsync();
            List<UserQuiz> data = new List<UserQuiz>();
            List<User> userList = await GetUserInfo();
            if(ratingData?.Any()==true)
            {
                foreach(var uq in ratingData)
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
            return View(data);
        }
    }
}
