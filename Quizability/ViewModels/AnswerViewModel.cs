using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.ViewModels
{
    public class AnswerViewModel
    {
        [Required(ErrorMessage = "Оберіть хоча б одну відповідь")]
        public int AnswerId { get; set; }
    }
}
