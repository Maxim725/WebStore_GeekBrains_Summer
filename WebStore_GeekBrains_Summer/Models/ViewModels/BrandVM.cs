using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore_GeekBrains_Summer.Models.ViewModels
{
    public class BrandVM : INamedEntity, IOrderedEntity
    {
        // Будет содержать число товаров
        public int Order { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        public int Amount { get; set; }
    }
}
