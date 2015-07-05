using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixMyCar.Repositories.interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void AddOrUpdate(T entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        T FindById(int id);
        IEnumerable<T> findAll();
    }
}
