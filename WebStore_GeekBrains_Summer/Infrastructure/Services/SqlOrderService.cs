using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Infrastructure.Services
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Order CreateOrder(OrderVM orderModel, CartVM cartModel, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            Order order;

            using (var transaction = _context.Database.BeginTransaction())
            {
                order = new Order()
                {
                    Address = orderModel.Address,
                    Name = orderModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in cartModel.Items)
                {
                    var productVM = item.Key;
                    var product = _context.Products.FirstOrDefault(p => p.Id.Equals(productVM.Id));
                    
                    if (product == null)
                        throw new InvalidOperationException("Продукт не найден в базе");

                    var orderItem = new OrderItem
                    {
                        Price = product.Price,
                        Quantity = item.Value,
                        Order = order,

                        Product = product
                    };

                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                transaction.Commit();
            }

            return order;
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include("User")
                .Include("OrderItems")
                .FirstOrDefault(o => o.Id.Equals(id));
        }

        public IEnumerable<Order> GetUserOrders(string userName)
        {
            return _context.Orders
                .Include("User")
                .Include("OrderItems")
                .Where(o => o.User.UserName.Equals(userName))
                .ToList();
        }
    }
}
