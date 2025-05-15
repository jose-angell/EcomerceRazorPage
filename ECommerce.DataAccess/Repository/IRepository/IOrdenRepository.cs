using ECommerce.Models;


namespace ECommerce.DataAccess.Repository.IRepository
{
    public interface IOrdenRepository: IRepository<Orden>
    {
        void Update(Orden orden);
    }
}
