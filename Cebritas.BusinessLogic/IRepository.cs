namespace Cebritas.BusinessLogic {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> : IDisposable where T : class {
        /// <summary>
        /// Get single element by id
        /// </summary>
        /// <param name="id">Element's id</param>
        T Get(long id);

        /// <summary>
        /// Filter by expression
        /// </summary>
        /// <param name="filter">Conditions</param>
        /// <param name="orderBy">Order by</param>
        IEnumerable<T> Filter(Expression<Func<T, bool>> filter = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// Insert a record to database
        /// </summary>
        /// <param name="item">Inser item in database</param>
        T Insert(T item);

        /// <summary>
        /// Delete database record by id
        /// </summary>
        /// <param name="id">Record id</param>
        int Delete(T id);

        /// <summary>
        /// Update record in database
        /// </summary>
        /// <param name="item">Item that will be updated</param>
        int Update(T item);

        /// <summary>
        /// Saves all realized changes
        /// </summary>
        void SaveChanges();
    }
}