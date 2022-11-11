using BE_TEST.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_TEST.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IQueryRepository<Domain.Entities.Employee> QueryEmployeeRepository { get; }
        ICommandRepository<Domain.Entities.Employee> CommandEmployeeRepository { get; }
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
