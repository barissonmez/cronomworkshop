using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Cronom.Web.Data.DBInteractions.Contracts;

namespace Cronom.Web.Data.DBInteractions.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CronomDBContext _dbContext;
        private readonly IDbSet<T> _dbset;

        public Repository(CronomDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public void Delete(Func<T, bool> predicate)
        {
            IEnumerable<T> objects = _dbset.Where<T>(predicate).AsEnumerable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
        }

        public T GetById(Guid id)
        {
            return _dbset.Find(id);
        }

        public T Get(Func<T, bool> where)
        {
            return _dbset.Where(where).FirstOrDefault<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        public IEnumerable<T> GetMany(Func<T, bool> where)
        {
            return _dbset.Where(where).ToList();
        }

        public IEnumerable<T> QueryObjectGraph(System.Linq.Expressions.Expression<Func<T, bool>> filter, string children1, string children2)
        {
            return _dbset.Include(children1).Include(children2).Where(filter);
        }
    }
}