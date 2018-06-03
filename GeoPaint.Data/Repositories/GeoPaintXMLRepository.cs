using System;
using System.Collections.Generic;
using System.Linq;
using GeoPaint.Data.Contracts;
using GeoPaint.Entities;
using System.Linq.Expressions;
using GeoPaint.Data.Contexts;
using System.ComponentModel.Composition;

namespace GeoPaint.Data.Repositories
{
    [Export(typeof(IRepository<EntityBase>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GeoPaintXMLRepository<T> : RepositoryBase, IRepository<T> where T : EntityBase
    {
        readonly GeoPaintXMLContext _xmlContext;
        IList<T> _entities;

        protected IList<T> XmlEntities
        {
            get
            {
                return _entities ?? (_entities = _xmlContext.Set<T>());
            }
        }

        public IQueryable<T> Entities
        {
            get
            {
                return XmlEntities.AsQueryable();
            }
        }

        public GeoPaintXMLRepository(GeoPaintXMLContext xmlContext)
        {
            _xmlContext = xmlContext;
        }



        public void Insert(T entity)
        {
            if (entity.EntityId == default(int))
            {
                var lastEntity = Entities.LastOrDefault();
                if (lastEntity != null)
                    entity.EntityId = lastEntity.EntityId + 1;
                else
                    entity.EntityId = 1;


            }

            XmlEntities.Add(entity);
            _xmlContext.SaveChanges(XmlEntities);
            //return entity;
        }

        public void Update(T entity)
        {
            Delete(entity.EntityId);
            Insert(entity);
        }
        public void Delete(int entityId)
        {
            var entityToDelete = Get(entityId);
            Delete(entityToDelete);
        }

        public void Delete(T entity)
        {
            if (XmlEntities.Any(i => i.EntityId == entity.EntityId))
            {
                XmlEntities.Remove(entity);
                _xmlContext.SaveChanges(XmlEntities);
            }
        }

        public T Get(int entityId)
        {
            return Entities.FirstOrDefault(x => x.EntityId == entityId);
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            return Entities.ToList();
        }


    }
}
