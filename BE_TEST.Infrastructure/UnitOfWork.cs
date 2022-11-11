using BE_TEST.Domain.Entities;
using BE_TEST.Domain.Interfaces;
using BE_TEST.Domain.Interfaces.Repositories;
using BE_TEST.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_TEST.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;

        private readonly DbContext _dbContext;

        private IQueryRepository<Domain.Entities.Employee> _queryEmployeeRepository;
        private ICommandRepository<Domain.Entities.Employee> _commandEmployeeRepository;

        public IQueryRepository<Employee> QueryEmployeeRepository
        {
            get
            {
                if( _queryEmployeeRepository == null)
                {
                    _queryEmployeeRepository = new QueryRepository<Domain.Entities.Employee>(_dbContext);
                }
                return _queryEmployeeRepository;
            }
        }

        public ICommandRepository<Employee> CommandEmployeeRepository
        {
            get
            {
                if (_commandEmployeeRepository == null)
                {
                    _commandEmployeeRepository = new CommandRepository<Domain.Entities.Employee>(_dbContext);
                }
                return _commandEmployeeRepository;
            }
        }

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            var affectedRecords = _dbContext.SaveChanges();

            if (affectedRecords == default)
            {
                throw new DbUpdateException("Transaction has been completed with 0 row(s) affected.");
            }

            return affectedRecords;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var affectedRecords = await _dbContext.SaveChangesAsync(cancellationToken);

            if (affectedRecords == default)
            {
                throw new DbUpdateException("Transaction has been completed with 0 row(s) affected.");
            }

            return affectedRecords;
        }
    }
}
