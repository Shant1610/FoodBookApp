using Food_Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Food_Blog.Data;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
    {

    }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }

}