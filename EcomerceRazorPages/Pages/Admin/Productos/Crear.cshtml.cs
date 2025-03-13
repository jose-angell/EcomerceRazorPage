using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceRazorPages.Pages.Admin.Productos
{
    public class CrearModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CrearModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [BindProperty]
        public Producto Producto { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImagenSubida { get; set; }
        public IActionResult OnGet()
        {
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
            if(ImagenSubida != null)
            {
                string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath,"productos");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImagenSubida.FileName;
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                //Restricciones: tamaño y formato
                if (ImagenSubida.Length > 2* 1024 * 1024)
                {
                    ModelState.AddModelError("ImagenSubida", "El tamaño maximo permitido es de 2 MB.");
                    return Page();
                }
                //Extensikones permitidas
                var allowedExtensions = new[] {".jpg",".jpeg",".png", ".gif"} ;
                if (!allowedExtensions.Contains(Path.GetExtension(ImagenSubida.FileName).ToLower())) 
                {
                    ModelState.AddModelError("ImagenSubida", "El archivo debe ser una imagen (.jpg, .jpeg, .png, .gif).");
                    return Page();
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create)) 
                {    
                    ImagenSubida.CopyTo(fileStream);
                }
                Producto.Imagen = "/productos/" + uniqueFileName;
            }
            Producto.FechaCreacion = DateTime.Now;
            _unitOfWork.Producto.Add(Producto);
            _unitOfWork.Save();
            // Usar TemData para mostrar el mensaje en la pagina de indice
            TempData["Success"] = "Producto creado con exito";
            return RedirectToPage("Index");
        }
    }
}
