using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Infrastructure.ViewComponents
{
    /*
     *  Если имя компонента изменить на кастомное, то и папку, 
     *  где хранится представление нужно тоже поменять, также вызов компонента должен происходить от этого имени, 
     *  то есть:
     *   @await Component.InvokeAsync("Cats")
     *   <vc:Cats></vc:Cats>
     */
    //[ViewComponent(Name = "Cats")]
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public CategoriesViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Categories = GetCategories();
            return View(Categories);
        }

        private List<CategoryVM> GetCategories()
        {
            var categories = _productService.GetCategories();

            // Получаем и заполняем родительские категории
            var parentSections = categories.Where(p => !p.ParentId.HasValue).ToArray();
            var parentCategories = new List<CategoryVM>();
            foreach(var parentCategory in parentSections)
            {
                parentCategories.Add(
                    new CategoryVM()
                    {
                        Id = parentCategory.Id,
                        Name = parentCategory.Name,
                        Order = parentCategory.Order,
                        ParentCategories = null
                    });
            }

            // Получаем и заполняем дочерние категории
            foreach(var categoryVM in parentCategories)
            {
                var childCategories = categories.Where(p => p.ParentId == categoryVM.Id);

                foreach(var childCategory in childCategories)
                {
                    categoryVM.ChildCategories.Add(
                        new CategoryVM()
                        {
                            Id = childCategory.Id,
                            Name = childCategory.Name,
                            Order = childCategory.Order,
                            ParentCategories = categoryVM
                        });
                }

                categoryVM.ChildCategories = categoryVM.ChildCategories.OrderBy(c => c.Order).ToList();
            }

            parentCategories = parentCategories.OrderBy(c => c.Order).ToList();

            return parentCategories;
        }
    }
}
