using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore_GeekBrains_Summer.Models.ViewModels
{
    public class CategoryVM : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<CategoryVM> ChildCategories { get; set; }
        public CategoryVM ParentCategories { get; set; }

        public CategoryVM()
        {
            ChildCategories = new List<CategoryVM>();
        }

    }
}
