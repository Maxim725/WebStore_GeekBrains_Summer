using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.ViewModels;

namespace WebStore_GeekBrains_Summer.Infrastructure.Services
{
    public class InMemoryEmployeeService : IEmployeeService
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
        public void AddNew(EmployeeVM model)
        {
            model.Id = _empls.Max(e => e.Id) + 1;
            _empls.Add(model);
        }

        public void Commit()
        {
            // nothing
        }

        public void Delete(int id)
        {
            var delModel = GetById(id);
            if (delModel != null)
            {
                _empls.Remove(delModel);
            }
            else
                return;
        }

        public IEnumerable<EmployeeVM> GetAll()
        {
            return _empls;
        }

        public EmployeeVM GetById(int id)
        {
            return _empls.FirstOrDefault(e => e.Id.Equals(id));
        }
    }
}
