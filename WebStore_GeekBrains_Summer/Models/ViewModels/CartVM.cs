using System.Collections.Generic;
using System.Linq;

namespace WebStore_GeekBrains_Summer.Models.ViewModels
{
    public class CartVM
    {
        public Dictionary<ProductVM, int> Items { get; set; }

        public int ItemsCount => Items?.Sum(x => x.Value) ?? 0;
    }
}
