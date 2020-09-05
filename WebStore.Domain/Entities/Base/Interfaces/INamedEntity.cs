using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entities.Base.Interfaces
{
    interface INamedEntity : IBaseEntity
    {
        public string Name { get; set; }
    }
}
