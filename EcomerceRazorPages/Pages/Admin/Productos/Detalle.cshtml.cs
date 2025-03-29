using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceRazorPages.Pages.Admin.Productos
{
    public class DetalleModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetalleModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Producto Producto { get; set; }
        public IActionResult OnGet(int id)
        {
            Producto = _unitOfWork.Producto.GetFirstOrDefault(x => x.Id == id);
            if(Producto == null)
            {
                return NotFound();
            }
           Producto.Categoria = _unitOfWork.Categoria.GetFirstOrDefault(x => x.Id == Producto.CategoriaId);
            return Page();
        }
    }
}
