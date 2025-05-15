using ECommerce.Models;


namespace ECommerce.DataAccess.Repository.IRepository
{
    public interface IDetalleOrdenRepository: IRepository<DetalleOrden>
    {
        void Update(DetalleOrden detalleOrden);
    }
}
