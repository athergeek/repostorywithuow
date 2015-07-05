using FixMyCar.Entities;
using FixMyCar.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixMyCar.Repositories.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbContext dbContext;
        protected DbSet<T> set;
        protected IUnitOfWork _uow;

        public BaseRepository(IUnitOfWork uow)
        {
            this._uow = uow;
            this.dbContext = this._uow.Context;
            this.set = this.dbContext.Set<T>();
        }

        public BaseRepository():this(new BaseUnitOfWork<FixMyCarEntities>())
        {
            this.set = this.dbContext.Set<T>();
        }

        public void AddOrUpdate(T entity)
        {
            EntityState state = this.dbContext.Entry(entity).State;
            switch (state)
            {
                case EntityState.Modified: { this.dbContext.Entry<T>(entity).State = EntityState.Modified; break; }
                case EntityState.Added: { this.dbContext.Entry<T>(entity).State = EntityState.Added; break; }
                default: { break; }
            }
            this.set.Add(entity);
        }

        public void Delete(T entity)
        {
            // This is necessary to deal with disconnected object graph...
            bool isDetached = this.dbContext.Entry(entity).State == EntityState.Detached;
            if (isDetached)
            {
                this.set.Attach(entity);
            }
            this.set.Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            
            foreach (var entity in entities)
            {
                this.Delete(entity);
            }
        }

        public T FindById(int id)
        {
            return this.set.Find(id);
        }

        public IEnumerable<T> findAll()
        {
            return this.set.ToList<T>();
        }

        public void save() {
            this._uow.Save();
        }

        public void Dispose()
        {
            
        }
    }
}
