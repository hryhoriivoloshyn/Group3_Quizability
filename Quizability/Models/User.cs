using System;
using System.Collections.Generic;

#nullable disable

namespace Quizability.Models
{
    public partial class User
    {
        public User()
        {
            UserAchievements = new HashSet<UserAchievement>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }

        public virtual ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
