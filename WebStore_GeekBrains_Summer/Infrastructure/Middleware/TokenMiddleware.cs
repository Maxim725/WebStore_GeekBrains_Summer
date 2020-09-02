using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore_GeekBrains_Summer.Infrastructure.Middleware
{
    public class TokenMiddleware
    {
        private const string correctToken = "qwerty123";

        public RequestDelegate Next { get; }

        public TokenMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["referenceToken"];

            // если нет точкена, то ничего не едлаем, передаём запрос дальше по конвейеру
            if (string.IsNullOrWhiteSpace(token))
            {
                await Next.Invoke(context);
                return;
            }
            if (token.Equals(correctToken))
            {
                // обработка токена
                await Next.Invoke(context);
            }
            else
            {
                await context.Response.WriteAsync("Uncorrect token");
            }
        }
    }
}
