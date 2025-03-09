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
        private readonly ICategoriaRepository _dbCategoria;
        public IndexModel(ICategoriaRepository dbCategoria)
        {
            _dbCategoria = dbCategoria;
        }
        public IEnumerable<Categoria> Categorias { get; set; } = default!;
        public void  OnGet()
        {
            Categorias = _dbCategoria.GetAll();
        }
        public async Task<IActionResult> OnPostDeleteAsync([FromBody] int id)
        {
            var categoria =  _dbCategoria.Find(id);
            if (categoria == null)
            {
                TempData["Error"] = "Categoria No encontrada";
                return RedirectToPage("Index");
                //return NotFound();
            }
            _dbCategoria.Remove(categoria);
            _dbCategoria.Save();
            TempData["Success"] = "Categoria elimminada con Exito";
            return new JsonResult(new { success = true });
        }
    }
}
