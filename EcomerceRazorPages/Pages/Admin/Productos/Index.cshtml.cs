using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceRazorPages.Pages.Admin.Productos
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Producto> Productos { get; set; } = default!;
        public void OnGet()
        {
            Productos = _unitOfWork.Producto.GetAll("Categoria");
        }
        public async Task<IActionResult> OnPostDelete([FromBody] int id)
        {
            var producto = _unitOfWork.Producto.GetFirstOrDefault(c => c.Id == id);
            if (producto == null)
            {
                TempData["Error"] = "Producto No encontrado";
                return RedirectToPage("Index");
                //return NotFound();
            }
            _unitOfWork.Producto.Remove(producto);
            _unitOfWork.Save();
            TempData["Success"] = "Producto elimminado con Exito";
            return new JsonResult(new { success = true });
        }
    }
}
