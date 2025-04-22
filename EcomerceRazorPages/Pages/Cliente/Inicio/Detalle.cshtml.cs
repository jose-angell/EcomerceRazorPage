using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

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
                Producto = _unitOfWork.Producto.GetFirstOrDefault(x => x.Id == id, "Categoria"),
                ProductoId = id
            };

            if (CarritoCompra == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            //if (Cantidad < 1 || Cantidad > Producto.CantidadDisponible)
            //{
            //    ModelState.AddModelError("Cantidad", $"Debe ingresar un valor entre 1 y {Producto.CantidadDisponible}");
            //    return Page();
            //}
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                CarritoCompra.ApplicationUserId = claim.Value;
                _unitOfWork.CarritoCompra.Add(CarritoCompra);
                _unitOfWork.Save();


                TempData["Success"] = $"{CarritoCompra.Cantidad} unidades(es) añadidas al carrito.";
                return RedirectToPage("/Index");
            }

            // Aqui se maneja la logica de agregar al carrito
            return Page();
        }
    }
}
