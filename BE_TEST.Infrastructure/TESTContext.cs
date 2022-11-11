using BE_TEST.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BE_TEST.Infrastructure
{
    public class TESTContext : DbContext
    {
        public readonly IConfiguration _configuration;

        public TESTContext(IConfiguration configuration, DbContextOptions<TESTContext> options) : base(options)
        {
            _configuration = configuration;
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #region
        public virtual DbSet<Employee>? Employees { get; set; }
        #endregion
    }
}
