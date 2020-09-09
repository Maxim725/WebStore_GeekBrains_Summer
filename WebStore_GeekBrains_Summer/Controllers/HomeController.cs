using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore_GeekBrains_Summer.Infrastructure.ActionFilters;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Controllers
{
    //[SimpleActionFilter]
    public class HomeController : Controller
    {
        IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [SimpleActionFilter]
        public IActionResult Index()
        {
            //var products = _productService.GetProducts(
            //    new ProductFilter() { BrandId = null, CategoryId = null });

            //var model = new CatalogVM()
            //{
            //    BrandId = null,
            //    CategoryId = null,
            //    Products = products.Select(p => new ProductVM()
            //    {
            //        Id = p.Id,
            //        ImageUrl = p.ImageUrl,
            //        Name = p.Name,
            //        Order = p.Order,
            //        Price = p.Price
            //    }).OrderBy(p => p.Order)
            //      .ToList()
            //};

            return View(/*model*/);

            // Можно вернуть строку
            //return Content("Hellow world!");

            // Можно пернуть пустой результат
            //return new EmptyResult();

            // Можно вернуть Файл
            /*eturn FileContentResult(new byte[] { 1, 2, 3, 4 }, );*/

            // Можно перенаправить куда-то
            //return Redirect("http://google.com");
            //return new RedirectResult("url");

            // Можно перенаправить внутри вэб приложения
            //return RedirectToAction("Shop", "Home");

            // А также
            //return new JsonResult("");
            //return StatusCode(500);
            //return NotFound();
        }

        public IActionResult Blog()
        {
            return View();
        }

        //[Route("blog-single")]
        public IActionResult BlogSingle() 
        {
            return View();
        }


        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public new IActionResult NotFound()
        {
            return View();
        }


    }
}
