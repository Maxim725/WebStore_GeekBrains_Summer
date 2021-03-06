using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL;
using WebStore.Domain;
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

            services.AddDbContext<WebStoreContext>(options => options
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            /*
             *  ��������� ������������ ��������� ������������� ������������ ���
             * 
                Transient: ��� ������ ��������� � ������� ��������� ����� ������ �������. 
                � ������� ������ ������� ����� ���� ��������� ��������� � �������, 
                �������������� ��� ������ ��������� ����� ����������� ����� ������.
                �������� ������ ���������� ����� �������� �������� ��� ����������� ��������, 
                ������� �� ������ ������ � ���������

                Scoped: ��� ������� ������� ��������� ���� ������ �������. �� ���� ���� � ������� ������ ������� ���� 
                ��������� ��������� � ������ �������, �� ��� ���� ���� ���������� 
                ����� �������������� ���� � ��� �� ������ �������.

                Singleton: ������ ������� ��������� ��� ������ ��������� � ����,
                ��� ����������� ������� ���������� ���� � ��� �� ����� ��������� ������ �������
             
             */
            // ��������� ���������� �����������
            // ������ IEmployeeService ����� ���������� �� InMemoryEmployeeService
            services.AddSingleton<IEmployeeService, InMemoryEmployeeService>();
            services.AddSingleton<IStudentService, InMemoryStudentService>();

            // ������ ������ ��������������� ������ ��� ��� ��������� (��� ����� http �������)
            services.AddScoped<IProductService, SqlProductService>();

            // ���������� ����������� IOrderService
            services.AddScoped<IOrderService, SqlOrderService>();

            // ������������ ���������� �������������
            //services.AddScoped<IEmployeeService, InMemoryEmployeeService>(); // ����� ����� � ���� ������

            // ������ ������������
            //services.AddTransient<IEmployeeService, InMemoryEmployeeService>(); // ����� ����� � ���� ��������� � �������

            // ��� ����������� �������� ����� �������

            // ��������� ������ �������������� ������������ ���������, � ���� ����� ����������� �� 3-�
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WebStoreContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => // �������������
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;

                // Lockout setup
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;

            });

            services.ConfigureApplicationCookie(options => // �������������
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                //options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login";
                options.LoginPath = "/Account/Logout";
                options.LoginPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;


            });

            // ��������� ��� �������
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CookieCartService>();
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

            app.UseRouting();
            
            // �����: ������� �������� ����� UseStaticFiles, ������ ���, ���� ������������ �� ������������, 
            // �� ��� ����� �� �������� ����� ����� ������ �������� � js-��������
            app.UseAuthentication();
            app.UseAuthorization(); // �������� ����������� ������ ���� ����� ��� UseRouting
            // ��������� Middleware
            //app.UseMiddleware<TokenMiddleware>();


            var get_str = _configuration["CustomeHelloWorld"];

            //var get_str_loglevel_def = _configuration["Logging:LogLevel:Default"];


            app.UseEndpoints(endpoints =>
            {
                // ��������� ���������� �������
                endpoints.MapAreaControllerRoute(
                   name: "default",
                   areaName: "Admin",
                   pattern: "Admin/{controller=Product}/{action=Show}/{id?}");
                
                // ��������� ���� ��������
                //endpoints.MapControllerRoute(
                //    name: "areas",
                //    pattern: "{area:exist}/{controller=Product}/{action=Show}/{id?}");

                //endpoints.MapDefaultControllerRoute(); // ������� ������
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

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
