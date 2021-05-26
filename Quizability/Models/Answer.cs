using System;
using System.Collections.Generic;

#nullable disable

namespace Quizability.Models
{
    public partial class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
