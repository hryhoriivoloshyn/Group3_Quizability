using System;
using System.Collections.Generic;

#nullable disable

namespace Quizability.Models
{
    public partial class Achievement
    {
        public Achievement()
        {
            UserAchievements = new HashSet<UserAchievement>();
        }

        public int AchievementId { get; set; }
        public string AchievementName { get; set; }
        public string AchievementText { get; set; }
        public int? QuizId { get; set; }

        public virtual ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
