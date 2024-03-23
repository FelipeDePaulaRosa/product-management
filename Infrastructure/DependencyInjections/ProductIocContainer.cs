using Infrastructure.Contexts;
using CrossCutting.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjections
{
    public static class ProductIocContainer
    {
        private static IConfiguration _configuration;
        
        public static void RegisterProductDependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;

            RegisterDatabases(services);
            RegisterServices(services);
        }

        private static void RegisterDatabases(IServiceCollection services)
        {
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
        }
        
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<INotificationContext, NotificationContext>();
        }
    }
}