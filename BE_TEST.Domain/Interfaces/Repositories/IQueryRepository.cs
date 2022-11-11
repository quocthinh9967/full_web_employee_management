using BE_TEST.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BE_TEST.Domain.Interfaces.Repositories
{
    public interface IQueryRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll(List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false);

        Task<IEnumerable<TEntity>> GetAllAsync(List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false);

        TEntity GetById(int id, List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false);

        Task<TEntity> GetByIdAsync(int id, List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false);

        IQueryable<TEntity> Find(List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false);

        IQueryable<TEntity> InitQuery(List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false);
    }
}
