using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Entity.Abstract;

namespace DataAccess.Abstract
{
    public interface IBaseDal<TEntity> where TEntity : class, IEntity, new()
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        void Update(TEntity entity);
        //void Delete(Expression<Func<TEntity, bool>> filter);
        void PermanentDelete(Expression<Func<TEntity, bool>> filter);
    }
}

