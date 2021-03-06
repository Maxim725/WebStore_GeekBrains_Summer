﻿using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; } // сгенерирует внешний ключ бд
        public virtual Product Product { get; set; } // сгенерирует внешний ключ БД
    }
}
