using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Controllers
{
    
    public class StudentsController : Controller
    {

        private readonly IStudentService _students;

        public StudentsController(IStudentService students)
        {
            _students = students;
        }
        public IActionResult List()
        {
            return View(_students.GetAll());
        }

        public IActionResult Student(int id)
        {
            var student = _students.GetById(id);
            if (student != null)
                return View(student);
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new StudentVM());

            var model = _students.GetById(id.Value);

            if (model == null)
                return NotFound(); // 404

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(StudentVM model)
        {
            if(model.Id > 0)
            {
                var dbItem = _students.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();

                dbItem.Id = model.Id;
                dbItem.Name = model.Name;
                dbItem.Surname = model.Surname;
                dbItem.Patronymic = model.Patronymic;
                dbItem.Rating = model.Rating;

                if (model.Labs == null)
                    dbItem.Labs = new List<LabsVM>();
                else
                    dbItem.Labs = model.Labs;
            }
            else
            {
                _students.AddNew(model);
            }

            _students.Commit();

            // редирект на список студентов
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                var model = _students.GetById(id);
                if(model != null)
                    _students.Delete(id);

            }

            return RedirectToAction("List");
        }
    }
}
