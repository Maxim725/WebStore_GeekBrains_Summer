using System.Collections.Generic;

namespace WebStore_GeekBrains_Summer.Models.ViewModels
{
    public class CatalogVM
    {
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<ProductVM> Products { get; set; }
    }
}
