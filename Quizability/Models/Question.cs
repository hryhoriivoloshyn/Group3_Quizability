using System;
using System.Collections.Generic;

#nullable disable

namespace Quizability.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string ImageSrc { get; set; }
        public double? QuestionPoints { get; set; }
        public int? QuestionType { get; set; }
        public int QuizId { get; set; }

        public virtual Quize Quiz { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
