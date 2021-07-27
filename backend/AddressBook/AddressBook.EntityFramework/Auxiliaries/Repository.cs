using AddressBook.Core.Auxiliaries.Entities;
using AddressBook.Core.Auxiliaries.Repositories;
using AddressBook.EntityFramework.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.EntityFramework.Auxiliaries
{
    public class Repository<TEntity> : IRepository<TEntity>
         where TEntity : class, IEntity, new()
    {
        protected readonly AddressBookContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructor that uses the context with the database connection to the object
        /// </summary>
        /// <param name="context"></param>
        public Repository(AddressBookContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        protected virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected async virtual void SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public TEntity Add(TEntity t)
        {
            if (t == null) throw new ArgumentNullException("Repository.Add entity");

            var result = _dbSet.Add(t);

            SaveChanges();

            return t;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> items)
        {
            if (items != null && items.Count() > 0)
            {
                _dbSet.AddRange(items);
                SaveChanges();
            }
            return items;
        }

        public async Task<TEntity> AddAsync(TEntity t)
        {
            if (t == null) throw new ArgumentNullException("Repository.Add entity");

            var result = await _dbSet.AddAsync(t);

            SaveChangesAsync();

            return t;
        }

        public TEntity AddOrUpdate(TEntity t)
        {
            if (t == null) throw new ArgumentNullException("Repository.AddOrUpdate entity");

            if (t.Id == 0)
                return Add(t);
            else
                return Update(t);
        }

        public async Task<TEntity> AddOrUpdateAsync(TEntity t)
        {
            if (t == null) throw new ArgumentNullException("Repository.AddOrUpdateAsync entity");

            if (t.Id == 0)
                return await AddAsync(t);
            else
                return await UpdateAsync(t);
        }

        public async Task DeleteAsync(int Id)
        {
            TEntity t = await _dbSet.Where(i => i.Id == Id).FirstOrDefaultAsync();
            _dbSet.Remove(t);

            SaveChangesAsync();
        }

        public void Delete(int Id)
        {
            TEntity t = _dbSet.Where(i => i.Id == Id).FirstOrDefault();
            if (t != null)
                _dbSet.Remove(t);

            SaveChanges();
        }

        public async Task DeleteAsync(TEntity t)
        {
            if (t == null) throw new ArgumentNullException("Repository.DeleteAsync entity");
            await DeleteAsync(t.Id);
        }

        public void Delete(TEntity t)
        {
            if (t == null) throw new ArgumentNullException("Repository.Delete entity");
            Delete(t.Id);
        }

        public void DeleteAll(List<TEntity> ts)
        {
            _dbSet.RemoveRange(ts);
            SaveChanges();
        }

        public IQueryable<TEntity> Fetch()
        {
            return _dbSet;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            SetIncludes(includes);
            return _dbSet.Where(predicate).ToList();
        }

        private void SetIncludes(params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var inc in includes)
            {
                _dbSet.Include(inc);
            }
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            SetIncludes(includes);
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public TEntity First(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            //SetIncludes(includes);
            var query = _context.Set<TEntity>().AsQueryable();

            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return query.FirstOrDefault(predicate);
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            SetIncludes(includes);
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            SetIncludes(includes);
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            SetIncludes(includes);
            return await _dbSet.ToListAsync();
        }

        public TEntity GetById(int Id, params Expression<Func<TEntity, object>>[] includes)
        {
            return First(x => x.Id == Id, includes);
        }

        public async Task<TEntity> GetByIdAsync(int Id, params Expression<Func<TEntity, object>>[] includes)
        {
            return await FirstAsync(x => x.Id == Id, includes);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            SetIncludes(includes);
            return _dbSet.Single(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            SetIncludes(includes);
            return await _dbSet.SingleAsync(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            SetIncludes(includes);
            return _dbSet.SingleOrDefault(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            SetIncludes(includes);
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }
        //TODO - should sanitize the input of the properties
        public TEntity Update(TEntity t)
        {
            TEntity existing = _context.Set<TEntity>().Find(t.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(t);
                SaveChanges();
            }

            return existing;
        }

        public virtual TEntity Update(TEntity t, params Expression<Func<TEntity, object>>[] properties)
        {
            //SetIncludes(properties);
            var query = _context.Set<TEntity>().AsQueryable();

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            TEntity existing = query.Where(x => x.Id == t.Id).FirstOrDefault();  //query.Where(x => x.Id == t.Id).FirstOrDefault(); //GetById(t.Id, properties);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(t);

                foreach (var prop in properties)
                {
                    //If navigation property are only one object
                    if (prop.Body.Type.GetInterface(nameof(System.Collections.IEnumerable)) == null)
                    {
                        IEntity dbObj = existing.GetType().GetProperties().Where(p => p.PropertyType == prop.Body.Type).FirstOrDefault().GetValue(existing, null) as IEntity;
                        IEntity newObj = t.GetType().GetProperties().Where(p => p.PropertyType == prop.Body.Type).FirstOrDefault().GetValue(t, null) as IEntity;

                        if (newObj != null && newObj.Id == 0)
                        {
                            _context.Entry(newObj).State = EntityState.Added;
                        }
                        else
                        {
                            if (newObj != null)
                            {
                                _context.Entry(dbObj).CurrentValues.SetValues(newObj);
                            }
                        }
                    }
                    //If navigation property is list of objects
                    else
                    {
                        IEnumerable<IEntity> dbObjs = existing.GetType().GetProperties().Where(p => p.PropertyType == prop.Body.Type).FirstOrDefault().GetValue(existing, null) as IEnumerable<IEntity>;
                        IEnumerable<IEntity> newObjs = t.GetType().GetProperties().Where(p => p.PropertyType == prop.Body.Type).FirstOrDefault().GetValue(t, null) as IEnumerable<IEntity>;

                        foreach (var dbobj in dbObjs)
                        {
                            var newObj = newObjs.Where(x => x.Id == dbobj.Id).FirstOrDefault();
                            if (newObj == null)
                            {
                                _context.Remove(dbobj);
                            }
                        }

                        foreach (var newObj in newObjs)
                        {
                            //Put Id of parent-convention foreeign key name always in format EntityNameId
                            newObj.GetType().GetProperty($"{t.GetType().Name}Id").SetValue(newObj, t.Id);

                            if (newObj.Id == 0)
                            {
                                _context.Entry(newObj).State = EntityState.Added;
                            }
                            else
                            {
                                var dbObj = dbObjs.Where(d => d.Id == newObj.Id).FirstOrDefault();
                                _context.Entry(dbObj).CurrentValues.SetValues(newObj);
                                _context.Entry(dbObj).State = EntityState.Modified;
                            }
                        }
                    }
                }
                SaveChanges();
            }
            return query.Where(x => x.Id == t.Id).FirstOrDefault(); ;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> items)
        {
            items.ToList().ForEach(t =>
            {
                TEntity existing = _context.Set<TEntity>().Find(t.Id);
                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(t);
                }
            });

            SaveChanges();

            return items;
        }

        public async Task<TEntity> UpdateAsync(TEntity t)
        {
            if (t == null) throw new ArgumentNullException("Repository.Update entity");

            TEntity existing = await _context.Set<TEntity>().FindAsync(t.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(t);
                SaveChangesAsync();
            }

            return existing;
        }

    }
}
