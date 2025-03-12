using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceRazorPages.Pages.Admin.Productos
{
    public class CrearModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public CrearModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Producto Producto { get; set; } = default!;
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnpostAsync()
        {
            //valida si el nombre ya existe
            if (_unitOfWork.Producto.ExisteNombre(Producto.Nombre))
            {
                ModelState.AddModelError("Producto.Nombre", "El nombre del producto ya existe. Por favor elige otro");
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Producto.FechaCreacion = DateTime.Now;
            _unitOfWork.Producto.Add(Producto);
            _unitOfWork.Save();
            // Usar TemData para mostrar el mensaje en la pagina de indice
            TempData["Success"] = "Producto creado con exito";
            return RedirectToPage("Index");
        }
    }
}
