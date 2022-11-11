using BE_TEST.Domain.Base;
using BE_TEST.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_TEST.Infrastructure.Repositories
{
    public class CommandRepository<TEntity> : ICommandRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public CommandRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public void Add(params TEntity[] entities)
        {
            PerformDbOperation(_dbSet.Add, entities);
        }

        public void Delete(params TEntity[] entities)
        {
            PerformDbOperation(_dbSet.Remove, entities);
        }

        public void Restore(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = false;
            }

            PerformDbOperation(_dbSet.Update, entities);
        }

        public void TemporaryDelete(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }

            PerformDbOperation(_dbSet.Update, entities);
        }

        public void Update(params TEntity[] entities)
        {
            PerformDbOperation(_dbSet.Update, entities);
        }

        private void PerformDbOperation(Func<TEntity, EntityEntry<TEntity>> dbOperation, params TEntity[] entities)
        {
            if (dbOperation == null)
            {
                throw new ArgumentNullException(nameof(dbOperation));
            }

            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            if (entities.Any(ex => ex == null))
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                dbOperation(entity);
            }
        }
    }
}
