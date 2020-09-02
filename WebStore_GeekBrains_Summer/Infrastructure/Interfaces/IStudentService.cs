using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Infrastructure.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<StudentVM> GetAll();

        StudentVM GetById(int id);

        void Commit();

        void AddNew(StudentVM model);

        void Delete(int id);
    }
}
