using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        ICategoriaRepository Categoria { get; }
        IProductoRepository Producto { get; }
        ICarritoCompraRepository CarritoCompra { get; }
        IOrdenRepository Orden { get; }
        IDetalleOrdenRepository DetalleOrden { get; }

        void Save();
    }
}
