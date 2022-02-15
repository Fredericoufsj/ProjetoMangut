using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoMangut.Models;

namespace ProjetoMangut.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

            Database.EnsureCreated();

        }

        public DbSet<Produto> Produto { get; set; }

        public DbSet<Compras> Compras { get; set; }
    }
}