using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Orders()
        {
            string name = _httpContextAccessor.HttpContext.User.Identity.Name ?? "";
            var orders = _orderService.GetUserOrders(name).Select(o => new OrderVM()
            {
                Address = o.Address,
                Name = o.Name,
                Phone = o.Phone
            });


            return View(orders);
        }
    }
}
