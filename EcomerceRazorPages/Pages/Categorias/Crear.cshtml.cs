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
            //valida si el nombre ya existe
            bool nombreExiste = _context.Categorias.Any(c => c.Nombre == Categoria.Nombre);
            if (nombreExiste)
            {
                ModelState.AddModelError("Categoria.Nombre", "El nombre de la categoria ya existe. Por favor elige otro");
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Categoria.FechaCreacion = DateTime.Now;
            _context.Categorias.Add(Categoria);
            await _context.SaveChangesAsync();
            // Usar TemData para mostrar el mensaje en la pagina de indice
            TempData["Success"] = "Categoria creada con exito";
            return RedirectToPage("Index");
        }
    }
}
