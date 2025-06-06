using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ECommerceRazorPages.Pages.Cliente.Carrito
{
    [Authorize]
    public class ResumenModel : PageModel
    {
        public IEnumerable<CarritoCompra> ListaCarritoCompra { get; set; }
        [BindProperty]
        public Orden Orden { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public ResumenModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Orden = new Orden();    
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); // obtiene el usuario
            if (claim != null)//valida si hay una persona logeada
            {
                ListaCarritoCompra = _unitOfWork.CarritoCompra.GetAll(
                    filter: u => u.ApplicationUserId == claim.Value,
                    includePropierties: "Producto,Producto.Categoria"
                    );

                foreach (var itemCarrito in ListaCarritoCompra)
                {
                    Orden.TotalOrden += (double)(itemCarrito.Producto.Precio * itemCarrito.Cantidad);
                }
                //Obtener los datos del usuario del Repositorio de ApplicationUser
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.
                    GetFirstOrDefault(u => u.Id ==  claim.Value);
                Orden.NombreUsuario = applicationUser.Nombres + " " + applicationUser.Apellidos;
                Orden.DireccionEnvio = applicationUser.Direccion;
                Orden.Telefono = applicationUser.PhoneNumber;
                Orden.InstruccionesAdicionales = Orden.InstruccionesAdicionales;
            }

        }

        public void OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); // obtiene el usuario
            if (claim != null)//valida si hay una persona logeada
            {
                ListaCarritoCompra = _unitOfWork.CarritoCompra.GetAll(
                    filter: u => u.ApplicationUserId == claim.Value,
                    includePropierties: "Producto,Producto.Categoria"
                    );

                foreach (var itemCarrito in ListaCarritoCompra)
                {
                    Orden.TotalOrden += (double)(itemCarrito.Producto.Precio * itemCarrito.Cantidad);
                }
                //Obtener los datos del usuario del Repositorio de ApplicationUser
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.
                    GetFirstOrDefault(u => u.Id == claim.Value);
                Orden.Estado = CNT.EstadoPendiente;
                Orden.FechaOrden = DateTime.Now;
                Orden.UsuarioId = claim.Value;
                Orden.NombreUsuario = applicationUser.Nombres + " " + applicationUser.Apellidos;
                Orden.DireccionEnvio = Orden.DireccionEnvio;
                Orden.Telefono = Orden.Telefono;
                Orden.InstruccionesAdicionales = Orden.InstruccionesAdicionales;
                _unitOfWork.Orden.Add(Orden);
                _unitOfWork.Save();

                // aqui se agrega detalleOrden
                foreach(var item in ListaCarritoCompra)
                {
                    DetalleOrden detalleOrden = new DetalleOrden()
                    {
                        ProductoId = item.ProductoId,
                        OrdenId = Orden.Id,
                        NombreProducto = item.Producto.Nombre,
                        Precio =  (double)item.Producto.Precio,
                        Cantidad = item.Cantidad
                    };
                    _unitOfWork.DetalleOrden.Add(detalleOrden);
                    _unitOfWork.Save();
                }
                //Limpia el carrito de  compras
                _unitOfWork.CarritoCompra.RemoveRange(ListaCarritoCompra);
                _unitOfWork.Save();
            }

        }
    }
}
