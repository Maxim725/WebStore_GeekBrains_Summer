using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore_GeekBrains_Summer.Infrastructure.ActionFilters;
using WebStore_GeekBrains_Summer.Infrastructure.Interfaces;
using WebStore_GeekBrains_Summer.Infrastructure.Middleware;
using WebStore_GeekBrains_Summer.Infrastructure.Services;

namespace WebStore_GeekBrains_Summer
{
    public class Startup
    {
        private readonly IConfiguration _configuration; // ��������� �������
        public Startup(IConfiguration config)
        {
            _configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        // ����������� 1 ��� � ��������� �������, ������� ����� � ����������
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => 
            {
                //options.Filters.Add(typeof(SimpleActionFilter)); // ���������� �� ����

                // ������������
                options.Filters.Add(new SimpleActionFilter()); // ����������� �� �������
            });

            // ��������� ���������� �����������
            // ������ IEmployeeService ����� ���������� �� InMemoryEmployeeService
            services.AddSingleton<IEmployeeService, InMemoryEmployeeService>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        
        // ����������� ��� ������ ������� (��� �������� �������� ������ �������) middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            // ����� ��������� ������� ���������� ������� ���������!!!!
            
            // ���� ������ ��������� � ������ ����������
            if (env.IsDevelopment())
            {
                // ����������� ������ ��� ���������, ������� �������� ������� ������
                app.UseDeveloperExceptionPage();
            }

            // ���������� ����������� ��������� ������� ����� � wwwroot, �� ����, �� ������ � ��� ���������� ����� url ������
            app.UseStaticFiles();
            // ��������� Middleware
            app.UseMiddleware<TokenMiddleware>();
            var get_str = _configuration["CustomeHelloWorld"];

            //var get_str_loglevel_def = _configuration["Logging:LogLevel:Default"];

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapDefaultControllerRoute(); // ������� ������
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

            // ��������, ���� ������������ url
            //app.UseWelcomePage();
            
            // �������� ��� ����������� url
            app.UseWelcomePage("/welcome");

            // ����������� Map Run Use � ���������

            // Map
            app.Map("/index", (p) => {
                p.Run(async context =>
                {
                    await context.Response.WriteAsync("response");
                });
            });

            // Use - ����� ���������� ������, � ����� � �� ����������
            UseMiddlewareSample(app);

            // Run - ��������� ��������� ������� � �� ������� ������ ������ �� ���������
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

    }
}
