using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GeekBrains_Summer.ViewModels;

namespace WebStore_GeekBrains_Summer.Infrastructure.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Получение списка сотрудников
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployeeVM> GetAll();

        EmployeeVM GetById(int id);

        void Commit();

        void AddNew(EmployeeVM model);

        void Delete(int id);
    }
}
