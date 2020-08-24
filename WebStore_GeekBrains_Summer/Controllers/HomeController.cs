using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore_GeekBrains_Summer.ViewModels;

namespace WebStore_GeekBrains_Summer.Controllers
{
    // прочитать о конвенции соглассованности в MVC
    public class HomeController : Controller
    {
        private readonly List<EmployeeVM> _empls = new List<EmployeeVM>()
        {
            new EmployeeVM()
            {
                Id = 1,
                FirstName = "a"
            },
            new EmployeeVM()
            {
                Id = 2,
                FirstName = "a"
            },
            new EmployeeVM()
            {
                Id = 3,
                FirstName = "a"
            }
        };
        public IActionResult Index()
        {
            //return View("Hello from controller");
            return View();
        }

        public IActionResult Index2()
        {
            return View("Hello from index 2");
        }

        public IActionResult Emplyees()
        {
            return View(_empls);
        }

        public IActionResult Details(int id)
        {
            var empl = _empls.First(i => i.Id == id) ?? null;

            if (empl == null)
                return NotFound();

            return View(empl);
        }
    }
}
