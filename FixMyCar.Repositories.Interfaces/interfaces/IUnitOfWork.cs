using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixMyCar.Repositories.interfaces
{
    public interface IUnitOfWork : IDisposable 
    {
        int Save();
        DbContext Context { get; }
    }
}
