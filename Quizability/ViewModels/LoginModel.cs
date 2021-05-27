using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizability.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Вкажіть Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вкажіть пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
