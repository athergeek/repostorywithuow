using FixMyCar.Repositories.interfaces;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Validation;
using System.Diagnostics;


namespace FixMyCar.Repositories.Repositories
{
    public class BaseUnitOfWork<T> : IUnitOfWork where T : DbContext, new()
    {
        private readonly T context;

        public BaseUnitOfWork()
        {
            this.context = new T();
        }

        public BaseUnitOfWork(T dbcontext)
        {
            this.context = dbcontext;
        }

        public int Save()
        {  
            int ret = 0;
            try
            {
                ret = this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }


                return ret;
        }



        public DbContext Context
        {
            get { return this.context; }
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
