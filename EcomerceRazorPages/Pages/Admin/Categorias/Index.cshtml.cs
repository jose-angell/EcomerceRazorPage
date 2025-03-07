using ECommerce.DataAccess;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceRazorPages.Pages.Admin.Categorias
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
                TempData["Error"] = "Categoria No encontrada";
                return RedirectToPage("Index");
                //return NotFound();
            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Categoria elimminada con Exito";
            return new JsonResult(new { success = true });
        }
    }
}
