﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Infrastructure.Services
{
    public class CookieCartService : ICartService
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        public Cart Cart
        {
            get
            {
                var cookie = _httpContextAccessor
                                .HttpContext
                                .Request
                                .Cookies[_cartName];
                string json = string.Empty;
                Cart cart = null;

                if(cookie == null)
                {
                    cart = new Cart() { Items = new List<CartItem>() };
                    json = JsonConvert.SerializeObject(cart);

                    _httpContextAccessor
                        .HttpContext
                        .Response
                        .Cookies
                        .Append(
                            _cartName,
                            json,
                            new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(1)
                            });
                    return cart;
                }

                json = cookie;
                cart = JsonConvert.DeserializeObject<Cart>(json);

                _httpContextAccessor
                    .HttpContext
                    .Response
                    .Cookies
                    .Delete(_cartName);

                _httpContextAccessor
                    .HttpContext
                    .Response
                    .Cookies
                    .Append(
                    _cartName,
                    json,
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                return cart;
            }
            set
            {
                var json = JsonConvert.SerializeObject(value);

                _httpContextAccessor
                    .HttpContext
                    .Response
                    .Cookies
                    .Delete(_cartName);

                _httpContextAccessor
                    .HttpContext
                    .Response
                    .Cookies
                    .Append(
                    _cartName,
                    json,
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });
            }
        }

        public CookieCartService(IProductService productService,
            IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;

            _cartName = "cart" + (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                        ? _httpContextAccessor.HttpContext.User.Identity.Name
                        : "");
        }

        public void AddToCart(int id)
        {

            // Нюанс для работы с куками, надо сначала получить, а потом присвоить
            // var cart = Cart
            // Cart = cart

            var cart = Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
                item.Quantity++;
            else
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });


            Cart = cart;
        }

        public void DecrementFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item == null)
                return;

            if (item.Quantity > 0)
                item.Quantity--;
            if (item.Quantity == 0)
                cart.Items.Remove(item);

            Cart = cart;
        }

        public void RemoveAll()
        {
            Cart = new Cart { Items = new List<CartItem>() };
        }

        public void RemoveFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item == null)
                return;

            cart.Items.Remove(item);

            Cart = cart;
        }

        public CartVM TransformCart()
        {
            var products = _productService.GetProducts(new ProductFilter()
            {
                Ids = Cart.Items.Select(i => i.ProductId).ToList()
            }).Select(p => new ProductVM()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,


            }).ToList();

            var r = new CartVM
            {
                Items = Cart.Items.ToDictionary(
                        x => products.First(y => y.Id == x.ProductId),
                        x => x.Quantity)
            };

            return r;
        }
    }
}
