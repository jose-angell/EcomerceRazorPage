using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceRazorPages.Pages.Cliente.Inicio
{
    [Authorize]
    public class DetalleModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetalleModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
       public CarritoCompra CarritoCompra { get; set; }
        public IActionResult OnGet(int id)
        {
            CarritoCompra = new()
            {
                Producto = _unitOfWork.Producto.GetFirstOrDefault(x => x.Id == id, "Categoria")
            };

            if (CarritoCompra == null)
            {
                return NotFound();
            }
            return Page();
        }
        //public IActionResult OnPost()
        //{
        //    if(Cantidad <1 || Cantidad > Producto.CantidadDisponible)
        //    {
        //        ModelState.AddModelError("Cantidad", $"Debe ingresar un valor entre 1 y {Producto.CantidadDisponible}");
        //        return Page();
        //    }
        //    // Aqui se maneja la logica de agregar al carrito
        //    TempData["Success"] = $"{Cantidad} unidades(es) del Producto {Producto.Nombre} añadido al carrito.";
        //    return RedirectToPage("/Index");
        //}
    }
}
