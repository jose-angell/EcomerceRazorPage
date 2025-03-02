using EcomerceRazorPages.Data;
using EcomerceRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EcomerceRazorPages.Pages.Categorias
{
    public class EditarModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditarModel(ApplicationDbContext context)
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var categoriaDb = await _context.Categorias.FindAsync(Categoria.Id);
            if (categoriaDb == null)
            {
                return NotFound();
            }
            categoriaDb.Nombre = Categoria.Nombre;
            categoriaDb.OrdenVisualizacion = Categoria.OrdenVisualizacion;

            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
