using Autofac.Core;
using EticaretAPI.Application.Abstraction.Hubs;
using EticaretAPI.SinglarR.HubServices;
using Microsoft.Extensions.DependencyInjection;


namespace EticaretAPI.SinglarR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddScoped<IProductHubService, ProductHubService>();
            services.AddScoped<IOrderHubService, OrderHubService>();
            services.AddSignalR();
        }
    }
}
