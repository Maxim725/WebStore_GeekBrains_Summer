using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Infrastructure.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public BrandsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = GetBrands();
            return View(brands);
        }

        private IEnumerable<BrandVM> GetBrands()
        {
            var brands = _productService.GetBrands();

            var brandsMV = brands.Select(b => new BrandVM()
            {
                Id = b.Id,
                Name = b.Name,
                Order = b.Order,
                Amount = 20
                
            }).OrderByDescending(d => d.Order).ToList();

            return brandsMV;
        }
    }
}
