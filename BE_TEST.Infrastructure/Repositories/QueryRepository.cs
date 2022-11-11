using BE_TEST.Domain.Base;
using BE_TEST.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BE_TEST.Infrastructure.Repositories
{
    public class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public QueryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Find(List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            return InitQuery(filters, disableChangeTracker, includes, isGetDeleted);
        }

        public IEnumerable<TEntity> GetAll(List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false)
        {
            return InitQuery(null, disableChangeTracker, includes, isGetDeleted).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false)
        {
            return await InitQuery(null, disableChangeTracker, includes, isGetDeleted).ToListAsync();
        }

        public TEntity GetById(int id, List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false)
        {
            return InitQuery(new List<Expression<Func<TEntity, bool>>> { ex => ex.Id == id }, disableChangeTracker, includes, isGetDeleted).SingleOrDefault();
        }

        public async Task<TEntity> GetByIdAsync(int id, List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false)
        {
            return await InitQuery(new List<Expression<Func<TEntity, bool>>> { ex => ex.Id == id }, disableChangeTracker, includes, isGetDeleted).SingleOrDefaultAsync();
        }

        public IQueryable<TEntity> InitQuery(List<Expression<Func<TEntity, bool>>> filters = null, bool disableChangeTracker = true, List<Expression<Func<TEntity, object>>> includes = null, bool isGetDeleted = false)
        {
            var query = _dbSet.AsQueryable();

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            if (isGetDeleted)
            {
                query = query.Where(x => x.IsDeleted == true || x.IsDeleted == false);
            }
            else
            {
                query = query.Where(x => x.IsDeleted == false);
            }

            if (disableChangeTracker)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
    }
}
