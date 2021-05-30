using System;
using System.Collections.Generic;

#nullable disable

namespace Quizability.Models
{
    public partial class Quize
    {
        public Quize()
        {
            Questions = new HashSet<Question>();
            UserQuizzes = new HashSet<UserQuiz>();
        }

        public int QuizId { get; set; }
        public string QuizName { get; set; }
        public string Description { get; set; }
        public double? Rating { get; set; }
        public TimeSpan? QuizTime { get; set; }
        public bool Popular { get; set; }
        public int? Difficulty { get; set; }
        public int? TopicId { get; set; }
        public string ImageSrc { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}
