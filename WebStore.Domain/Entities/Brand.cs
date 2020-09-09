using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Table("Brands")] // Класс Brand будет проецироваться на таблицу Brand (указываем для явности и можем кастомные таблицы привязывать)
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        // С помощью этого синтаксиса смогут связаться продукты с брендами
        public virtual ICollection<Product> Products { get; set; }
    }
}
