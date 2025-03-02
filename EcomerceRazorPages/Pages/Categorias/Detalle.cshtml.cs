using EcomerceRazorPages.Data;
using EcomerceRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcomerceRazorPages.Pages.Categorias
{
    public class DetalleModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DetalleModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Categoria Categoria { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        { 
            Categoria = await _context.Categorias.FindAsync(id);
            if (Categoria == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
