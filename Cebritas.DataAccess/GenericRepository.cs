using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cebritas.BusinessLogic;
using Cebritas.General;
using Cebritas.General.Geo;

namespace Cebritas.DataAccess {
    public class GenericRepository<T> : IRepository<T> where T : class {
        internal CebraContext context;
        internal DbSet<T> dbSet;

        public GenericRepository() {
            this.context = CebraContext.GetInstance();
            this.dbSet = context.Set<T>();
        }

        public T Get(long id) {
            return dbSet.Find(id);
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) {
            IQueryable<T> query = this.dbSet;

            if (filter != null) {
                query = query.Where(filter);
            }
            if (orderBy != null) {
                return orderBy(query).ToList();
            } else {
                return query.ToList();
            }
        }

        public T Insert(T item) {
            T newEntry = this.dbSet.Add(item);
            this.context.SaveChanges();
            return newEntry;
        }

        public int Delete(T item) {
            if (context.Entry(item).State == System.Data.EntityState.Detached) {
                this.dbSet.Attach(item);
            }
            dbSet.Remove(item);
            return context.SaveChanges();
        }

        public int Update(T item) {
            if(item == null) {
                throw new CebraException("Null item not allowed");
            }
            dbSet.Attach(item);
            context.Entry(item).State = System.Data.EntityState.Modified;
            return context.SaveChanges();
        }

        public void SaveChanges() {
            this.context.SaveChanges();
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}