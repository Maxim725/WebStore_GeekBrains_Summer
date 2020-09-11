using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Infrastructure.Services
{
    public class InMemoryStudentService : IStudentService
    {
        private List<StudentVM> _students = new List<StudentVM>()
        {
            new StudentVM()
            {
                Id = 1,
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
                Id = 2,
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
                Id = 3,
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

        public void AddNew(StudentVM model)
        {
            model.Id = _students.Max(model => model.Id) + 1;
            _students.Add(model);
        }

        public void Commit()
        {
        }

        public void Delete(int id)
        {
            var delModel = GetById(id);

            if (delModel == null)
                return;

            _students.Remove(delModel);
        }

        public IEnumerable<StudentVM> GetAll()
        {
            return _students;
        }

        public StudentVM GetById(int id)
        {
            return _students.FirstOrDefault(e => e.Id == id);
        }
    }
}
