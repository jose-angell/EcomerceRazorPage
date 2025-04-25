using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //Metodos para CRUD
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> ? filter = null, string? includePropierties = null);
        T GetFirstOrDefault(Expression<Func<T,bool>> ? filter = null, string? includePropierties = null); // pasa una expresion
        bool ExisteNombre(string nombre);
    }
}
