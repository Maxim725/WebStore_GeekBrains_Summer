using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore_GeekBrains_Summer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();

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

        public IActionResult Shop()
        {
            return View();
        }

        public IActionResult ProductDetails()
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
