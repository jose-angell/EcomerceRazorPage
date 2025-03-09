using ECommerce.DataAccess;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceRazorPages.Pages.Admin.Categorias
{
    public class CrearModel : PageModel
    {
        private readonly ICategoriaRepository _bdCategoria;
        public CrearModel(ICategoriaRepository bdCategoria)
        {
            _bdCategoria = bdCategoria;
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
            //bool nombreExiste = _bdCategoria.Any(c => c.Nombre == Categoria.Nombre);
            //if (nombreExiste)
            //{
            //    ModelState.AddModelError("Categoria.Nombre", "El nombre de la categoria ya existe. Por favor elige otro");
            //    return Page();
            //}
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Categoria.FechaCreacion = DateTime.Now;
            _bdCategoria.Add(Categoria);
            _bdCategoria.Save();
            // Usar TemData para mostrar el mensaje en la pagina de indice
            TempData["Success"] = "Categoria creada con exito";
            return RedirectToPage("Index");
        }
    }
}
