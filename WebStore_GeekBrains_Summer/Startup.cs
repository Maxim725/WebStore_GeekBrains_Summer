using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL;
using WebStore_GeekBrains_Summer.Infrastructure.ActionFilters;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Infrastructure.Middleware;
using WebStore_GeekBrains_Summer.Infrastructure.Services;

namespace WebStore_GeekBrains_Summer
{
    public class Startup
    {
        private readonly IConfiguration _configuration; // интерфейс словаря
        public Startup(IConfiguration config)
        {
            _configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        // Выполняется 1 раз и добавляет сервисы, которые нужны в приложении
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => 
            {
                //options.Filters.Add(typeof(SimpleActionFilter)); // подключаем по типу

                // альтернатива
                options.Filters.Add(new SimpleActionFilter()); // подключение по объекту
            });

            services.AddDbContext<WebStoreContext>(options => options
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            /*
             *  Внедрение зависимостей позволяет разрабатывать слабосвязный код
             * 
                Transient: при каждом обращении к сервису создается новый объект сервиса. 
                В течение одного запроса может быть несколько обращений к сервису, 
                соответственно при каждом обращении будет создаваться новый объект.
                Подобная модель жизненного цикла наиболее подходит для легковесных сервисов, 
                которые не хранят данных о состоянии

                Scoped: для каждого запроса создается свой объект сервиса. То есть если в течение одного запроса есть 
                несколько обращений к одному сервису, то при всех этих обращениях 
                будет использоваться один и тот же объект сервиса.

                Singleton: объект сервиса создается при первом обращении к нему,
                все последующие запросы используют один и тот же ранее созданный объект сервиса
             
             */
            // Добавляем разрешение зависимости
            // Каждый IEmployeeService будет заменяться на InMemoryEmployeeService
            services.AddSingleton<IEmployeeService, InMemoryEmployeeService>();
            services.AddSingleton<IStudentService, InMemoryStudentService>();

            // Сервис должен пересоздаваться каждый раз при обращении (при новом http запросе)
            services.AddScoped<IProductService, SqlProductService>();

            // Альтернатива разрешения зависимоствей
            //services.AddScoped<IEmployeeService, InMemoryEmployeeService>(); // Время жизни в один запрос

            // Вторая альтернатива
            //services.AddTransient<IEmployeeService, InMemoryEmployeeService>(); // Время жизни в одно обращение к сервису

            // Они различаются временем жизни сервиса
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        
        // Выполняется при каждом запросе (это конвейер проверки логики запроса) middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            // Важно соблюдать порядок добавление модулей конвейера!!!!
            
            // Если проект находится в стадии разработки
            if (env.IsDevelopment())
            {
                // Подключение модуля для конвейера, который подробно выводит ошибку
                app.UseDeveloperExceptionPage();
            }

            // Добавление статических элементов которые лежат в wwwroot, то есть, мы сможем к ним обращаться через url запрос
            app.UseStaticFiles();
            
            // Кастомный Middleware
            app.UseMiddleware<TokenMiddleware>();


            var get_str = _configuration["CustomeHelloWorld"];

            //var get_str_loglevel_def = _configuration["Logging:LogLevel:Default"];

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapDefaultControllerRoute(); // краткий аналог
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=Home}/{action=Index}/{id?}");
                /*
                 * 
                 */
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!" + get_str);
                //});
            });

            // Заглушка, если неправильный url
            //app.UseWelcomePage();
            
            // Заглушка для конкретного url
            app.UseWelcomePage("/welcome");

            // Обработчики Map Run Use в конвейере

            // Map
            app.Map("/index", (p) => {
                p.Run(async context =>
                {
                    await context.Response.WriteAsync("response");
                });
            });

            // Use - может передавать запрос, а может и не передавать
            UseMiddlewareSample(app);

            // Run - завершает обработку запроса и не пускает запрос дальше по конвейеру
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Not Found");
            });
        }
        private void UseMiddlewareSample(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                bool isError = false;
                // ...
                if (isError)
                {
                    await context.Response
                        .WriteAsync("Error occured");
                }
                else
                {
                    await next.Invoke();
                }
            });
        }

        
        public class WebStoreContextFactory : IDesignTimeDbContextFactory<WebStoreContext>
        {
 
            public WebStoreContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<WebStoreContext>();
                optionsBuilder.UseSqlServer("Server=DESKTOP-BA9BA6R\\SQLEXPRESS;Initial Catalog=WebStore;Trusted_Connection=true;Connection Timeout=30;");

                return new WebStoreContext(optionsBuilder.Options);
            }
        }

    }
}
