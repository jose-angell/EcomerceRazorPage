using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace ECommerceRazorPages.Pages.Admin.Productos
{
    public class EditarModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public EditarModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [BindProperty]
        public Producto Producto { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }
        public IActionResult OnGet(int id)
        {
            Producto = _unitOfWork.Producto.GetFirstOrDefault(p => p.Id == id);
            if (Producto == null) { 
                return NotFound();
            }
            Categorias = _unitOfWork.Categoria.GetAll()
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre
            });
            //Validacion por si la tabla categorias no tiene categorias
            if (!Categorias.Any())
            {
                ModelState.AddModelError(string.Empty, "NO hay Categorias disponibles. Por favor, agregue categorias primer.");
            }
            return Page();
        }

        public async Task<IActionResult> OnpostAsync()
        {
            //valida si el nombre ya existe
            if (_unitOfWork.Producto.ExisteNombre(Producto.Nombre))
            {
                ModelState.AddModelError("Producto.Nombre", "El nombre del producto ya existe. Por favor elige otro");
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Procesar la imagen subida
            if (Producto.ImagenSubida != null)
            {
                string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "productos");
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Producto.ImagenSubida.FileName);
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                //Restricciones: tamaño y formato
                if (Producto.ImagenSubida.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImagenSubida", "El tamaño maximo permitido es de 2 MB.");
                    return Page();
                }
                //Extensikones permitidas
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(Path.GetExtension(Producto.ImagenSubida.FileName).ToLower()))
                {
                    ModelState.AddModelError("ImagenSubida", "El archivo debe ser una imagen (.jpg, .jpeg, .png, .gif).");
                    return Page();
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Producto.ImagenSubida.CopyTo(fileStream);
                }
                // Eliminar la imagen antetriro si existe
                if (!string.IsNullOrEmpty(Producto.Imagen))
                {
                    string olFilePath = Path.Combine(uploadFolder, Producto.Imagen);
                    if (System.IO.File.Exists(olFilePath))
                    {
                        System.IO.File.Delete(olFilePath);
                    }
                }
                Producto.Imagen = uniqueFileName;
            }else
            {
                var productoDesdeDb = _unitOfWork.Producto.GetFirstOrDefault(p => p.Id == Producto.Id);
                if (productoDesdeDb != null) {
                    Producto.Imagen = productoDesdeDb.Imagen;
                }
            }
            // Actualizar el producto en la base de datos
            _unitOfWork.Producto.Update(Producto);
            _unitOfWork.Save();
            // Usar TemData para mostrar el mensaje en la pagina de indice
            TempData["Success"] = "Producto actualizado con exito";
            return RedirectToPage("Index");
        }
    }
}
