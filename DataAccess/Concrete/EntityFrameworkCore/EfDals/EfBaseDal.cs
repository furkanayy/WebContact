using DataAccess.Abstract;
using Entity.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFrameworkCore.EfDals
{
    public class EfBaseDal<TEntity, TContext> : IBaseDal<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null ? context.Set<TEntity>() : context.Set<TEntity>().Where(filter);
            }
        }

        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void PermanentDelete(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                var entity = context.Set<TEntity>().FirstOrDefault(filter);
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        //public void Delete(Expression<Func<TEntity, bool>> filter)
        //{
        //    using (var context = new TContext())
        //    {
        //        var deletedEntity = context.Set<TEntity>().FirstOrDefault(filter);
        //        var propertie = deletedEntity.GetType().GetProperty("Sil");
        //        propertie.SetValue(deletedEntity, true);
        //    }
        //}
    }
}