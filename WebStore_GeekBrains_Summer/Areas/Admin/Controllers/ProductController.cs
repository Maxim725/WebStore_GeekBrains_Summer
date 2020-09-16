using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admins")]
    //[Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Show()
        {
            List<ProductVM> products = _productService.GetProducts(new ProductFilter())
                                                .Select(e => new ProductVM()
                                                {
                                                    Id = e.Id,
                                                    Name = e.Name,
                                                    ImageUrl = e.ImageUrl,
                                                    Order = e.Order,
                                                    Price = e.Price
                                                })
                                                .ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product modelDTO = _productService.GetProducts(new ProductFilter()).FirstOrDefault(p => p.Id == id);

            if (modelDTO == null)
                return RedirectToAction("Show");

            ProductVM model = new ProductVM();

            model.Id = modelDTO.Id;
            model.Name = modelDTO.Name;
            model.ImageUrl = modelDTO.ImageUrl;
            model.Order = modelDTO.Order;
            model.Price = modelDTO.Price;

            return View("Edit",model);
        }

        [HttpPost]
        public IActionResult Edit(ProductVM model)
        {
            Product mDTO = _productService.GetProducts(new ProductFilter()).FirstOrDefault(e => e.Id == model.Id);

            if (mDTO == null)
                return NotFound();

            mDTO.Name = model.Name;
            mDTO.ImageUrl = model.ImageUrl;
            mDTO.Order = model.Order;
            mDTO.Price = model.Price;

            return RedirectToAction("Show");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Product modelDTO = _productService.GetProducts(new ProductFilter()).FirstOrDefault(p => p.Id == id);
            if (modelDTO == null)
                return NotFound();

            ProductVM model = new ProductVM()
            {
                Id = modelDTO.Id,
                Name = modelDTO.Name,
                ImageUrl = modelDTO.ImageUrl,
                Order = modelDTO.Order,
                Price = modelDTO.Price
            };


            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return NotFound();
            //return RedirectToAction("Show");
        }
    }
}
