using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebStore.Domain.Entities.Identity;

namespace WebStore.DAL.Context;

public class WebStoreDB:IdentityDbContext<User, Role, string>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Employer> Employers{ get; set; }
    public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options)
    {
        
    }
}