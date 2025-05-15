using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class DetalleOrden
    {
        [Key]
        public int Id { get; set; }
        public int OrdenId { get; set; }

        [ForeignKey("OrdenId")]//relacion
        public Orden Orden { get; set; }

        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]//relacion
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public double Precio { get; set; }

        public string NombreProducto { get; set; }

    }
}
