using Microsoft.Extensions.Options;

namespace Wrail.GPM.FE.MVC.AppSettings.Data.SQLServer.Settings;

public class DbConnectionString : Wrail.GPM.Database.SQLServer.Service.IConnectionString
{
    private readonly Options.DbConnectionString dbConnectionString;

    public DbConnectionString(IOptions<Options.DbConnectionString> options)
    {
        dbConnectionString = options.Value;
    }

    public string ConnectionString => dbConnectionString.ConnectionString;
}
