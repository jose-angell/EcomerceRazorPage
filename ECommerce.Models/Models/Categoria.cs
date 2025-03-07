using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requrerido.")]
        [StringLength(100, ErrorMessage = "El Nombre no puede superar los 100 caracteres.")]
        [Display(Name = "Nombre de la Categoria")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Orden de visualizacion es requererido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El Orden debe ser mayor a 0.")]
        [Display(Name = "Orden de visualizacion")]
        public int OrdenVisualizacion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now; 
    }
}
