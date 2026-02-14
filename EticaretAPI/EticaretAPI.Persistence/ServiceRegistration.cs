
using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Services.Authentication;
using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.Repositories.Basket;
using EticaretAPI.Application.Repositories.BasketItem;
using EticaretAPI.Application.Repositories.CompletedOrderRep;
using EticaretAPI.Application.Repositories.CustomerRep;
using EticaretAPI.Application.Repositories.EndPoint;
using EticaretAPI.Application.Repositories.File;
using EticaretAPI.Application.Repositories.InvoiceFile;
using EticaretAPI.Application.Repositories.Menu;
using EticaretAPI.Application.Repositories.OrderRep;
using EticaretAPI.Application.Repositories.ProductImageFile;
using EticaretAPI.Application.Repositories.ProductRep;
using EticaretAPI.Domain.Entities.Identity;
using EticaretAPI.Persistence.Contexts;
using EticaretAPI.Persistence.Repositories;
using EticaretAPI.Persistence.Repositories.Basket;
using EticaretAPI.Persistence.Repositories.BasketItem;
using EticaretAPI.Persistence.Repositories.CompletedOrder;
using EticaretAPI.Persistence.Repositories.EndPoint;
using EticaretAPI.Persistence.Repositories.File;
using EticaretAPI.Persistence.Repositories.InvoiceFile;
using EticaretAPI.Persistence.Repositories.Menu;
using EticaretAPI.Persistence.Repositories.ProductImageFile;
using EticaretAPI.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EticaretAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration config)
        {
            // Bağlantı dizesini doğrudan IConfiguration'dan al
            services.AddDbContext<EticaretAPIDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<EticaretAPIDbContext>().AddDefaultTokenProviders();
            

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository,ProductImageFileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IFileReadRepository,FileReadRepository>();
            services.AddScoped<IFileWriteRepository,FileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceWriteRepository>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication,AuthService>();
            services.AddScoped<IBasketWriteRepository, BasketWriteReposityory>();
            services.AddScoped<IBasketReadRepository, BasketReadReposityory>();
            services.AddScoped<IBasketItemReadRepostiyory, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepostiyory, BasketItemWriteRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<ICompletedOrderReadRepository,CompletedOrderReadRepository>();
            services.AddScoped<ICompletedOrderWriteRepository,CompletedOrderWriteRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
            services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();
            services.AddScoped<IMenuReadRepositiory,MenuReadRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
            services.AddScoped<IAuthorizeEndPointService,AuthorizeEndPointService>();

        }
    }
}
