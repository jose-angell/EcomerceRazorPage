using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombrees es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string  Nombre { get; set; }

        [Required(ErrorMessage = "La Descripcion es obligatorio")]
        [StringLength(500, ErrorMessage = "La Descripcion no puede superar los 500 caracteres.")]
        public string Descripcion { get; set; }

        //[Required(ErrorMessage = "La Imagen es obligatorio")]
        //[MaxLength(500, ErrorMessage = "La ruta de la imagen no puede superar los 500 caracteres.")]
        public string? Imagen { get; set; }

        [NotMapped] // permite que este elemento se ignore al generar una migracion
        public IFormFile? ImagenSubida { get; set; }

        [Required(ErrorMessage = "El Precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero.")]
        public Decimal Precio { get; set; }

        [Required(ErrorMessage = "La Cantidad es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "La Cantidad no puede ser negativa.")]
        [Display(Name = "Cantidad Desponible")]
        public int CantidadDisponible { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        //Relacion entre categorias y productos
        [Required(ErrorMessage = "La categoria es obligatoria")]
        [Display(Name ="Categoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}
