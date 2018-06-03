using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeoPaint.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        T Get(int entityId);
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null);
        void Delete(T entity);
        void Delete(int entityId);
    }
}
