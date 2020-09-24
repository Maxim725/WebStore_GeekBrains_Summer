using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore_GeekBrains_Summer.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage = "Имя является обязательным")] // атрибут валидации
        [StringLength(100, MinimumLength = 2, ErrorMessage = "В имени должно быть не меньше 2-х символов и не больше 100 символов")] // атрибут валидации
        [Display(Name = "Имя")] // атрибут аннотации
        public string FirstName { get; set; }

    }
}
