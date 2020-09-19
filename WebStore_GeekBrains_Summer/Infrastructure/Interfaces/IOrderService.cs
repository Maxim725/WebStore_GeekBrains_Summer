using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string userName);
        Order GetOrderById(int id);

        Order CreateOrder(OrderVM orderModel, CartVM cartModel, string userName);
    }
}
