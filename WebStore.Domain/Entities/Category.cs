using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Table("Categories")]
    public class Category : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Родительская секция при наличии
        /// </summary>
        public int? ParentId { get; set; }
        public int Order { get; set; }

        // Связываем поле ParentId с ParentCategory смапим id с объектом (может  и автоматически)
        [ForeignKey("ParentId")] 
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
