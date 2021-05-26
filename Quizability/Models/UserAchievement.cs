using System;
using System.Collections.Generic;

#nullable disable

namespace Quizability.Models
{
    public partial class UserAchievement
    {
        public int AchievementId { get; set; }
        public int UserId { get; set; }
        public DateTime? ObtainingTime { get; set; }

        public virtual Achievement Achievement { get; set; }
        public virtual User User { get; set; }
    }
}
