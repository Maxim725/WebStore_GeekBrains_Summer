using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        [Key] // Будет первичным ключом
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Будет автоинкрементное свойство
        public int Id { get; set; }
    }
}
