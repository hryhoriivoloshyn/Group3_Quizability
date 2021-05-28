using System;
using System.Collections.Generic;

#nullable disable

namespace Quizability.Models
{
    public partial class UserQuiz
    {
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public double Points { get; set; }
        public DateTime StartTme { get; set; }
        public DateTime FinishTime { get; set; }
        public int RightAnswersAmount { get; set; }
        public bool Finished { get; set; }

        public virtual Quize Quiz { get; set; }
        public virtual User User { get; set; }
    }
}
