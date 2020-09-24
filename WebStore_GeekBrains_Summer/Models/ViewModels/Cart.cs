using Microsoft.AspNetCore.Http.Features;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore_GeekBrains_Summer.Models.ViewModels
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }

        public int ItemsCount => Items?.Sum(x => x.Quantity) ?? 0;
    }
}
