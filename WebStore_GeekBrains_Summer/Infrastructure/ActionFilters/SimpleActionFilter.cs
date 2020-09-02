using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore_GeekBrains_Summer.Infrastructure.ActionFilters
{
    /* Фильтр можно повесить как на метод, так и на контроллер и он будет отрабатывать на всех методах
     * Также можно на все контроллеры повесить через класс Startup метод ConfigureServices
     */
    public class SimpleActionFilter : Attribute, IActionFilter
    {
        // после Action метода
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        // до Action метода
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
