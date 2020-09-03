using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.ViewModels;

namespace WebStore_GeekBrains_Summer.Controllers
{
    // если ставим кастомный роут для контроллера, то для всех методов таже прописываем, но, если не прописан, 
    //то всё-равно можно прописывать кастомный роуты для методов
    [Route("users")]

    // такая конструкция позволит оставить в url название методов по-умолчанию!
    //[Route("users/[action]")] 

    // прочитать о конвенции соглассованности в MVC
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            // Внедрение зависимостей
            _employeeService = employeeService;
        }

        [Route("index")]
        public IActionResult Index()
        {
            //return View("Hello from controller");
            return View();
        }

        public IActionResult Index2()
        {
            return View("Hello from index 2");
        }
        [Route("list")]
        public IActionResult Employees()
        {
            return View(_employeeService.GetAll());
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            // Получаем сотрудника по id
            var empl = _employeeService.GetById(id);
            
            // Если такого не существует
            if (empl == null)
                return NotFound(); // 404

            return View(_employeeService.GetById(id));
        }

        [HttpGet]
        [Route("edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new EmployeeVM());

            var model = _employeeService.GetById(id.Value);

            if (model == null)
                return NotFound(); // 404

            return View(model);
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public IActionResult Edit(EmployeeVM model)
        {
            if(model.Id > 0) // если есть Id то редактируем модель
            {
                var dbItem = _employeeService.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();

                dbItem.Id = model.Id;
                dbItem.FirstName = model.FirstName;

            }
            else
            {
                _employeeService.AddNew(model);
            }

            _employeeService.Commit();

            // редирект на список сотрудников
            return RedirectToAction(nameof(Employees));
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                var model = _employeeService.GetById(id);
                if (model != null)
                    _employeeService.Delete(id);

            }

            return RedirectToAction("Employees");
        }
    }
}
