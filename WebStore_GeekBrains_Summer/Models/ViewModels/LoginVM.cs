using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore_GeekBrains_Summer.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        
        // Нужна, чтобы пользователь зарегестрировался и его вернуло на компонент, в котором он был
        public string ReturnUrl { get; set; }
    }
}
