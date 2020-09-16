using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;

namespace WebStore_GeekBrains_Summer.Infrastructure.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count = _cartService.Cart.ItemsCount;
            decimal price = 0;

            foreach (var item in _cartService.TransformCart().Items)
                price += item.Key.Price * item.Value;

            ViewBag.Price = price;
            ViewBag.Count = count;

            return View();
        }
        /*
         @if (ViewBag.Price == 0)
{
    <a asp-controller="Cart" asp-action="Details"><i class="fa fa-shopping-cart"></i> Cart</a>
}
else
{
    <a asp-controller="Cart" asp-action="Details"><i class="fa fa-shopping-cart"></i> Cart(@ViewBag.Count)(@ViewBag.Price)</a>
}
         */
    }
}
