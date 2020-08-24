using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore_GeekBrains_Summer.Models.ViewModels
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public double Rating { get; set; }
        public IEnumerable<LabsVM> Labs { get; set; }
        
    }
}
