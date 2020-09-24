using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.Domain.Entities;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;

namespace WebStore_GeekBrains_Summer.Areas.Admin.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Area("Admin")]
    //[Authorize(Roles = "Admins")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly WebStoreContext _webStoreContext;

        public ProductsController(IProductService productService, WebStoreContext webStoreContext)
        {
            _productService = productService;
            _webStoreContext = webStoreContext;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductsList()
        {
            var products = _productService.GetProducts(new ProductFilter());
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryId = _webStoreContext.Categories.Select(p => p.Id).ToList();
            ViewBag.BrandId = _webStoreContext.Brands.Select(p => p.Id).ToList();
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = _webStoreContext.Categories.Select(p => p.Id).ToList();
                ViewBag.BrandId = _webStoreContext.Brands.Select(p => p.Id).ToList();
                return View("Create", model);
            }

            using(var transaction = _webStoreContext.Database.BeginTransaction())
            {
                _webStoreContext.Products.Add(model);
                _webStoreContext.SaveChanges();
                transaction.Commit();
            }

            return RedirectToAction("ProductsList");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductByid(id);

            if (product == null)
                return NotFound();

            ViewBag.CategoryId = _webStoreContext.Categories.Select(p => p.Id).ToList();
            ViewBag.BrandId = _webStoreContext.Brands.Select(p => p.Id).ToList();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = _webStoreContext.Categories.Select(p => p.Id).ToList();
                ViewBag.BrandId = _webStoreContext.Brands.Select(p => p.Id).ToList();
                return View(model);
            }

            using(var transaction = _webStoreContext.Database.BeginTransaction())
            {
                var product = _webStoreContext.Products.FirstOrDefault(p => p.Id.Equals(model.Id)); 

                if(product == null)
                {
                    ViewBag.CategoryId = _webStoreContext.Categories.Select(p => p.Id).ToList();
                    ViewBag.BrandId = _webStoreContext.Brands.Select(p => p.Id).ToList();
                    return View(model);
                }


                _webStoreContext.Products.Remove(product);

                product = model;

                _webStoreContext.Products.Add(product);

                _webStoreContext.SaveChanges();

                transaction.Commit();
            }

            return RedirectToAction("ProductsList");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductByid(id);

            using(var transaction = _webStoreContext.Database.BeginTransaction())
            {
                _webStoreContext.Products.Remove(product);

                _webStoreContext.SaveChanges();
                transaction.Commit();
            }

            return RedirectToAction("ProductsList");
        }

    }
}
