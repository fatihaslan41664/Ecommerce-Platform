using Serilog.Core;
using Serilog.Events;

namespace EticaretAPI.API.Configuration.ColumnWriters
{
    public class UserNameColumnWriter : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var userNameProperty = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");

            if (userNameProperty.Value != null)
            {
                var username = userNameProperty.Value.ToString().Trim('"');

                // Yeni bir property olarak ekle
                var property = propertyFactory.CreateProperty("UserName", username);
                logEvent.AddPropertyIfAbsent(property);
            }
        }
    }
}
