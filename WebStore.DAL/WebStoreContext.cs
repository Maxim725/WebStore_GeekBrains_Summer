using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebStore.DAL
{

    public class WebStoreContext : IdentityDbContext<User>
    {
        public WebStoreContext(DbContextOptions/*<WebStoreContext>*/ options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
