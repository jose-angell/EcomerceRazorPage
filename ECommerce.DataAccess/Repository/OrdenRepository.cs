using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Models;


namespace ECommerce.DataAccess.Repository
{
    public class OrdenRepository : Repository<Orden>, IOrdenRepository
    {
        private readonly ApplicationDbContext _context;
        public OrdenRepository(ApplicationDbContext context): base(context) 
        {
            _context = context;
        }
        public void Update(Orden orden)
        {
            var objDesdeDb = _context.Ordenes.FirstOrDefault(c => c.Id == orden.Id);
            _context.Ordenes.Update(objDesdeDb);
        }
    }
}
