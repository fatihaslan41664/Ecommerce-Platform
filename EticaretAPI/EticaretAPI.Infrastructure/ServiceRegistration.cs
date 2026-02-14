using EticaretAPI.Application;
using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Storage;
using EticaretAPI.Application.Abstraction.Storage.Aws;
using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Application.Enums;
using EticaretAPI.Infrastructure.Services;
using EticaretAPI.Infrastructure.Services.Configurations;
using EticaretAPI.Infrastructure.Services.MailService;
using EticaretAPI.Infrastructure.Services.Storage;
using EticaretAPI.Infrastructure.Services.Storage.AWS;
using EticaretAPI.Infrastructure.Services.Storage.Local;
using EticaretAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using EticaretAPI.Application.Abstraction.Services.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastractureServices(this IServiceCollection service) 
        {
            service.AddScoped<IStorageServices,StorageServices>();
            service.AddScoped<ITokenHandler,TokenHandler>();
            service.AddScoped<IMailService,MailService>();
            service.AddScoped<IApplicationService, ApllicationService>();

        }
        public static void AddStorage<T>(this IServiceCollection ser) where T : class, IStorage
        {
            ser.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageTypes storageTypes)
        {
            switch (storageTypes)
            {
                case StorageTypes.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;

                case StorageTypes.AWS:
                    serviceCollection.AddScoped<IAWSStorage, AWSStorage>();
                    break;
            }
        }

    }
}
