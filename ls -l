using EticaretAPI.API.Extantations;
using EticaretAPI.API.Extensions;
using EticaretAPI.API.Filters;
using EticaretAPI.Application;
using EticaretAPI.Application.Validators.Products;
using EticaretAPI.Infrastructure;
using EticaretAPI.Infrastructure.Filters;
using EticaretAPI.Infrastructure.Services.Storage.AWS;
using EticaretAPI.Infrastructure.Services.Storage.Local;
using EticaretAPI.Persistence;
using EticaretAPI.SinglarR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();//Clientten gelen istekle httpcontext nesnesine katmanlardaki classlardan erişebilmeni sağlar
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastractureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();
builder.Services.AddControllers();
builder.Services.AddStorage<AWSStorage>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
    );
});
builder.Host.AddCustomSerilog(builder.Configuration);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<RoleFilter>();
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",
        options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateAudience = true, //hangi sitelerin kullanacagını belirtir
                ValidateIssuer = true, // tokeni kimin dagıtıcagını belirtir
                ValidateLifetime = true, // token süresi
                ValidateIssuerSigningKey = true, // tokenin security key i
                ValidAudience = builder.Configuration["Token:Audience"],
                ValidIssuer = builder.Configuration["Token:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                NameClaimType = ClaimTypes.Name
            };
        });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    var userName = context.User?.Identity?.IsAuthenticated == true
        ? context.User.Identity.Name
        : "Anonymous";

    using (LogContext.PushProperty("user_name", userName))
    {
        await next();
    }
});
app.MapHubsSerivce();
app.MapControllers();
app.Run();
