using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Orden
    {
        [Key]
        public int Id { get; set; }
        public string  UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double TotalOrden { get; set; }

        public string? TransaccionId { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        [Display(Name = "Instrucciones Adicionales")]
        public string InstruccionesAdicionales { get; set; }

        [Display(Name = "Fecha de Orden")]
        public DateTime FechaOrden { get; set; }


    }
}
