using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ECommerceRazorPages.Pages.Cliente.Carrito
{
    public class IndexModel : PageModel
    {

        public IEnumerable<CarritoCompra> ListaCarritoCompra { get; set; }
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
            }

        }
    }
}
