using BE_TEST.Domain.Interfaces.Repositories;
using BE_TEST.Domain.Interfaces;
using BE_TEST.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BE_TEST.Domain.Interfaces.Services;
using BE_TEST.Service.Services;
using System.Reflection;

namespace BE_TEST.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, TESTContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("TESTDb"))
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            return services;
        }
        public static IServiceCollection AddGenericRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
