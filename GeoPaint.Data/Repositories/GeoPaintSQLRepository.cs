using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Data.Contexts;
using GeoPaint.Data.Contracts;
using GeoPaint.Entities;

namespace GeoPaint.Data.Repositories
{
    [Export(typeof(IRepository<EntityBase>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GeoPaintSQLRepository<T> : RepositoryBase, IRepository<T> where T : EntityBase
    {
        private GeoPaintSQLDbContext _context;
        private DbSet<T> _dbSet;

        public GeoPaintSQLRepository(GeoPaintSQLDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int entityId)
        {
            var entityToDelete = _dbSet.Find(entityId);

            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public T Get(int entityId)
        {
            return _dbSet.Find(entityId);
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            return _dbSet.Where(filter);
        }


    }
}
