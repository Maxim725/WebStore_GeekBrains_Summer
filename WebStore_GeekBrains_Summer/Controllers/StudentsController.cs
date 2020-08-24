using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Controllers
{
    public class StudentsController : Controller
    {
        private IEnumerable<StudentVM> _students = new List<StudentVM>()
        {
            new StudentVM()
            {
                Id = 0,
                Name = "Егор",
                Surname = "aaa",
                Patronymic = "ddd",
                Rating = 0d,
                Labs = new List<LabsVM>()
                {
                    new LabsVM()
                    {
                        Id = 0,
                        Name = "Chemistry",
                        Theme = "Oxygen"
                    },
                    new LabsVM()
                    {
                        Id = 0,
                        Name = "Chemistry",
                        Theme = "Uran"
                    },
                    new LabsVM()
                    {
                        Id = 0,
                        Name = "Chemistry",
                        Theme = "Argon"
                    },
                    new LabsVM()
                    {
                        Id = 0,
                        Name = "Chemistry",
                        Theme = "Helium"
                    }
                }
            },
            new StudentVM()
            {
                Id = 1,
                Name = "John",
                Surname = "aasdasdaa",
                Patronymic = "ddasdasdd",
                Rating = 0d,
                Labs = new List<LabsVM>()
                {
                    new LabsVM()
                    {
                        Id = 0,
                        Name = "History",
                        Theme = "World war"
                    },
                    new LabsVM()
                    {
                        Id = 0,
                        Name = "History",
                        Theme = "World war 2"
                    },
                    new LabsVM()
                    {
                        Id = 0,
                        Name = "History",
                        Theme = "USSR"
                    },
                }
            },
            new StudentVM()
            {
                Id = 2,
                Name = "Ivan",
                Surname = "aaasdasda",
                Patronymic = "dasdasdadd",
                Rating = 0d,
                Labs = new List<LabsVM>()
                {
                    new LabsVM()
                    {
                        Id = 0,
                        Name = "Computer Science",
                        Theme = "Hardware"
                    },
                    new LabsVM()
                    {
                        Id = 0,
                        Name = "Computer Science",
                        Theme = "Firmware"
                    },
                    new LabsVM()
                    {
                        Id = 0,
                        Name = "Computer Science",
                        Theme = "Software"
                    },
                }
            }

        };
        public IActionResult List()
        {
            return View(_students);
        }

        public IActionResult Student(int id)
        {
            var student = _students.First(e => e.Id == id) ?? null;
            if (student != null)
                return View(student);
            else
                return NotFound();
        }
    }
}
