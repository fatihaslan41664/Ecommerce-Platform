using EticaretAPI.API.Configuration.ColumnWriters;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace EticaretAPI.API.Extantations
{
    public static class SerilogExtensions 
    {
        public static IHostBuilder AddCustomSerilog(this IHostBuilder host, IConfiguration configuration)
        {
            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);
            columnOptions.AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn
                {
                    ColumnName = "UserName",
                    PropertyName = "UserName",
                    DataType = SqlDbType.NVarChar,
                    DataLength = 100,
                    AllowNull = true
                }
            };
            var logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Enrich.FromLogContext()
                .Enrich.With<UserNameColumnWriter>()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("DefaultConnection"),
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = true
                    },
                    columnOptions: columnOptions
                )
                .CreateLogger();

            return host.UseSerilog(logger);
        }
    }
}