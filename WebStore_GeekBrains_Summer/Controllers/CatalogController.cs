using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;
        public CatalogController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Shop(int? categoryId, int? brandId)
        {
            var products = _productService.GetProducts(
                new ProductFilter() { BrandId = brandId, CategoryId = categoryId });

            var model = new CatalogVM()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Select(p => new ProductVM()
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    BrandName = p.Brand?.Name ?? string.Empty
                }).OrderBy(p => p.Order)
                  .ToList()
            };
            return View(model);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductByid(id);

            if (product == null)
                return NotFound();


            return View(new ProductVM()
            {
                Id = product.Id,
                Name = product.Name,
                Order = product.Order,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                BrandName = product.Brand?.Name ?? string.Empty
            });
        }
    }
}
