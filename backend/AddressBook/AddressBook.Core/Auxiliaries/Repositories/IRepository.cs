using AddressBook.Core.Auxiliaries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Core.Auxiliaries.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        IQueryable<TEntity> Fetch();
        Task<TEntity> AddAsync(TEntity t);
        TEntity Add(TEntity t);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> items);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> items);
        Task<TEntity> AddOrUpdateAsync(TEntity t);
        TEntity AddOrUpdate(TEntity t);
        void Delete(TEntity t);
        void Delete(int Id);
        Task DeleteAsync(TEntity t);
        Task DeleteAsync(int Id);
        void DeleteAll(List<TEntity> ts);
        Task<TEntity> UpdateAsync(TEntity t);
        TEntity Update(TEntity t);
        TEntity Update(TEntity t, params Expression<Func<TEntity, object>>[] properties);
        TEntity GetById(int ID, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        TEntity Single(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        TEntity First(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdAsync(int ID, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    }
}
