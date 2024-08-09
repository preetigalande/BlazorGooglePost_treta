
using Wrail.GPM.Database.SQLServer.Service;
using Wrail.GPM.FE.MVC.AppSettings.Data.SQLServer.Settings;

namespace Wrail.GPM.FE.MVC.DIExtensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {
        _ = services.AddScoped<IConnectionString, DbConnectionString>();

        return services;
    }
}
