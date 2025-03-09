using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repository.IRepository
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {
        void Update(Categoria categoria);
        void Save();
        Categoria Find(int id);
    }
}
