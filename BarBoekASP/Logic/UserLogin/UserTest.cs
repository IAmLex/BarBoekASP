using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BarBoekASP.Logic.UserLogin
{
    public class UserTest
    {
        public int UID { get; set; }
        [Display(Name = "E-mail*")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "U moet de email invullen.")]
        public string UEmail { get; set; }
        [Display(Name = "Password*")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "U moet de password invullen.")]
        public string Password { get; set; }
        [Display(Name = "Herhaal je password*")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "De password is niet correct.")]
        public string HerhaalPassword { get; set; }
    }
}
