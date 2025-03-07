using ECommerce.DataAccess;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EcomerceRazorPages.Pages.Admin.Categorias
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
                TempData["Error"] = "Categoria No encontrada";
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
                TempData["Error"] = "Categoria No encontrada";
                return RedirectToPage("Index");
                //return NotFound();
            }
            categoriaDb.Nombre = Categoria.Nombre;
            categoriaDb.OrdenVisualizacion = Categoria.OrdenVisualizacion;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Categoria Editada con exito";
            return RedirectToPage("Index");
        }
    }
}
