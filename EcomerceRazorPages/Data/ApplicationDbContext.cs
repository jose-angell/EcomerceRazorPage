using EcomerceRazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomerceRazorPages.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //here put the DbSets of all tables
        public DbSet<Categoria> Categorias { get; set; }
    }
}
