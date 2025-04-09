using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceRazorPages.Pages.Cliente.Inicio
{
    public class DetalleModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetalleModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Producto Producto { get; set; }
        [BindProperty]
        public int Cantidad { get; set; }
        public IActionResult OnGet(int id)
        {
            Producto = _unitOfWork.Producto.GetFirstOrDefault(x => x.Id == id, "Categoria");
            if (Producto == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if(Cantidad <1 || Cantidad > Producto.CantidadDisponible)
            {
                ModelState.AddModelError("Cantidad", $"Debe ingresar un valor entre 1 y {Producto.CantidadDisponible}");
                return Page();
            }
            // Aqui se maneja la logica de agregar al carrito
            TempData["Success"] = $"{Cantidad} unidades(es) del Producto {Producto.Nombre} añadido al carrito.";
            return RedirectToPage("/Index");
        }
    }
}
