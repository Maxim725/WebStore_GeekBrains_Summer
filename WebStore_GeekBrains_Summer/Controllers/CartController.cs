using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public IActionResult Details()
        {
            var model = new OrderDetailsVM()
            {
                CartVM = _cartService.TransformCart(),
                OrderVM = new OrderVM()
            };
            return View("Details", model);
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult AddToCart(int id, string returnUrl)
        {
            _cartService.AddToCart(id);
            return Redirect(returnUrl);
        }

        // Создание заказа
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut(OrderVM model)
        {
            if (ModelState.IsValid)
            {
                var orderResult = _orderService.CreateOrder(model, _cartService.TransformCart(), User.Identity.Name);
                _cartService.RemoveAll();
                return RedirectToAction("OrderConfirmed", new { id = orderResult.Id });
            }

            var detailsModel = new OrderDetailsVM()
            {
                CartVM = _cartService.TransformCart(),
                OrderVM = model
            };

            return View("Details", detailsModel);
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
