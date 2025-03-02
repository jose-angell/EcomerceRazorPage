using EcomerceRazorPages.Data;
using EcomerceRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcomerceRazorPages.Pages.Categorias
{
    public class CrearModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CrearModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Categoria Categoria { get; set; } = default!;
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnpostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Categoria.FechaCreacion = DateTime.Now;
            _context.Categorias.Add(Categoria);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
