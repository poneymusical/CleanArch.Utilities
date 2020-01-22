using CleanArch.Utilities.GenericCrud.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Utilities.Repository.EntityFrameworkCore.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEFCoreGenericRepository<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.AddScoped<DbContext, TDbContext>();
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            return services;
        }
    }
}