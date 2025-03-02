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
        public async Task<IActionResult> OnPostDeleteAsync([FromBody] int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true });
        }
    }
}
