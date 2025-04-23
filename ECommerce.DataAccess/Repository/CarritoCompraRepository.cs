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
    public class CarritoCompraRepository : Repository<CarritoCompra>, ICarritoCompraRepository
    {
        private readonly ApplicationDbContext _context;
        public CarritoCompraRepository(ApplicationDbContext context): base(context) 
        {
            _context = context;
        }

        public int DecrementarContador(CarritoCompra carritoCompra, int contador)
        {
            carritoCompra.Cantidad -= contador;
            _context.SaveChanges();
            return carritoCompra.Cantidad;
        }

        public int IncrementarContador(CarritoCompra carritoCompra, int contador)
        {
            carritoCompra.Cantidad += contador;
            _context.SaveChanges();
            return contador;
        }
    }
}
