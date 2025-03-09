using ECommerce.DataAccess;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceRazorPages.Pages.Admin.Categorias
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Categoria> Categorias { get; set; } = default!;
        public void  OnGet()
        {
            Categorias = _unitOfWork.Categoria.GetAll();
        }
        public async Task<IActionResult> OnPostDelete([FromBody] int id)
        {
            var categoria = _unitOfWork.Categoria.GetFirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                TempData["Error"] = "Categoria No encontrada";
                return RedirectToPage("Index");
                //return NotFound();
            }
            _unitOfWork.Categoria.Remove(categoria);
            _unitOfWork.Save();
            TempData["Success"] = "Categoria elimminada con Exito";
            return new JsonResult(new { success = true });
        }
    }
}
