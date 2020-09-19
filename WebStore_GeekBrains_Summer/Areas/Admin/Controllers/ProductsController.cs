using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize(Roles = "Admins")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
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

        

    }
}
