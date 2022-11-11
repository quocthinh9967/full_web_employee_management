using BE_TEST.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_TEST.Domain.Interfaces.Repositories
{
    public interface ICommandRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(params TEntity[] entities);

        void Update(params TEntity[] entities);

        void Delete(params TEntity[] entities);

        void TemporaryDelete(params TEntity[] entities);

        void Restore(params TEntity[] entities);
    }
}
