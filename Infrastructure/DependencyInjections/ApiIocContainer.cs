using CrossCutting.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjections
{
    public static class ApiIocContainer
    {
        public static void RegisterApiServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationContext, NotificationContext>();
        }
    }
}