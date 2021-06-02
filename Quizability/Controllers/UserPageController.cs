using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.Controllers
{
    [Authorize]
    public class UserPageController : Controller
    {
        Quizability_serverContext db;
        public UserPageController(Quizability_serverContext db)
        {
            this.db = db;
        }

        private async Task<List<UserAchievement>> GetAllAchivements()
        {
            string userEmail = this.User.Identity.Name;
            User threadUser = db.Users.Where(u => u.Email == userEmail).First();
            List<UserAchievement> data = new List<UserAchievement>();
            List<UserAchievement> tempData = await db.UserAchievements.Where(d=>d.UserId==threadUser.UserId).ToListAsync();
            if(tempData?.Any()==true)
            {
                foreach(var item in tempData)
                {
                    Achievement achievement = db.Achievements.Where(a => a.AchievementId == item.AchievementId).First();
                    data.Add(new UserAchievement()
                    {
                        UserId = item.UserId,
                        AchievementId = item.AchievementId,
                        ObtainingTime = item.ObtainingTime,
                        Achievement = achievement,
                        User = threadUser
                    });
                }
            }
            return data;
        }

        public async Task<ViewResult> Index()
        {
            var data = await GetAllAchivements();
            return View(data);
        }
    }
}
