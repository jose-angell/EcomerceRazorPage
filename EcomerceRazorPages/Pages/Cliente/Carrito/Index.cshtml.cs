using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ECommerceRazorPages.Pages.Cliente.Carrito
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<CarritoCompra> ListaCarritoCompra { get; set; }
        public double TotalCarrito { get; set; } = 0;
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)//valida si hay una persona logeada
            {
                ListaCarritoCompra = _unitOfWork.CarritoCompra.GetAll(
                    filter: u => u.ApplicationUserId == claim.Value,
                    includePropierties: "Producto,Producto.Categoria"
                    );

                foreach (var itemCarrito in ListaCarritoCompra) { 
                    TotalCarrito += (double)(itemCarrito.Producto.Precio * itemCarrito.Cantidad);
                }
            }

        }
        public IActionResult OnPostMas(int carritoId)
        {
            var carrito = _unitOfWork.CarritoCompra.GetFirstOrDefault(c => c.Id == carritoId);
            _unitOfWork.CarritoCompra.IncrementarContador(carrito, 1);
            return RedirectToPage("/Cliente/Carrito/Index");
        }
        public IActionResult OnPostMenos(int carritoId)
        {
            var carrito = _unitOfWork.CarritoCompra.GetFirstOrDefault(c => c.Id == carritoId);
            if(carrito.Cantidad == 1)
            {
                _unitOfWork.CarritoCompra.Remove(carrito);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.CarritoCompra.DecrementarContador(carrito, 1);
            }
            return RedirectToPage("/Cliente/Carrito/Index");
        }
        public IActionResult OnPostEliminar(int carritoId)
        {
            var carrito = _unitOfWork.CarritoCompra.GetFirstOrDefault(c => c.Id == carritoId);
            _unitOfWork.CarritoCompra.Remove(carrito);
            _unitOfWork.Save();
            return RedirectToPage("/Cliente/Carrito/Index");
        }
    }
}
