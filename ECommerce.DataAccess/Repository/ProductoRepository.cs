using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repository
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductoRepository(ApplicationDbContext context): base(context) 
        {
            _context = context;
        }

        public void Update(Producto producto)
        {
            var objProductDb = _context.Productos.FirstOrDefault(c => c.Id == producto.Id);
            objProductDb.Nombre = producto.Nombre;
            objProductDb.Descripcion = producto.Descripcion;
            objProductDb.Precio = producto.Precio;
            objProductDb.CantidadDisponible = producto.CantidadDisponible;
            objProductDb.CategoriaId = producto.CategoriaId;

            if (objProductDb.Imagen != null)
            {
                objProductDb.Imagen = producto.Imagen;
            }

        }
    }
}
