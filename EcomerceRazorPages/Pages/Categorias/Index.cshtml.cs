using EcomerceRazorPages.Data;
using EcomerceRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EcomerceRazorPages.Pages.Categorias
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Categoria> Categorias { get; set; } = default!;
        public async Task OnGetAsync()
        {
            Categorias = await _context.Categorias.OrderBy(c => c.OrdenVisualizacion).ToListAsync();
        }
    }
}
