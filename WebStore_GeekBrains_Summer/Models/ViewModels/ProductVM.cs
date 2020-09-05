using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore_GeekBrains_Summer.Models.ViewModels
{
    public class ProductVM : INamedEntity, IOrderedEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
