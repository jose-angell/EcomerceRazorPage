using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;


namespace ECommerce.DataAccess.Repository
{
    public class DetalleOrdenRepository : Repository<DetalleOrden>, IDetalleOrdenRepository
    {
        private readonly ApplicationDbContext _context;
        public DetalleOrdenRepository(ApplicationDbContext context): base(context) 
        {
            _context = context;
        }
        public void Update(DetalleOrden detalleOrden)
        {
            var objDesdeDb = _context.DetalleOrdenes.FirstOrDefault(c => c.Id == detalleOrden.Id);
            _context.DetalleOrdenes.Update(objDesdeDb);
        }
    }
}
