using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceRazorPages.Pages.Admin.Categorias
{
    public class EditarModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditarModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Categoria Categoria { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Categoria =  _unitOfWork.Categoria.GetFirstOrDefault(c => c.Id == id);
            if (Categoria == null)
            {
                TempData["Error"] = "Categoria No encontrada";
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var categoriaDb =  _unitOfWork.Categoria.GetFirstOrDefault(c => c.Id == Categoria.Id);
            if (categoriaDb == null)
            {
                TempData["Error"] = "Categoria No encontrada";
                return RedirectToPage("Index");
                //return NotFound();
            }
            categoriaDb.Nombre = Categoria.Nombre;
            categoriaDb.OrdenVisualizacion = Categoria.OrdenVisualizacion;

            _unitOfWork.Save();
            TempData["Success"] = "Categoria Editada con exito";
            return RedirectToPage("Index");
        }
    }
}
