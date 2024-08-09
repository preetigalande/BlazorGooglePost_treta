namespace Wrail.GPM.FE.MVC.AppSettings.Data.SQLServer.Options
{
    public class DbConnectionString: Wrail.GPM.Database.SQLServer.Service.IConnectionString
    {
        public const string ConfigPath = "Data:SQLServer";

        public string ConnectionString { get; set; } = string.Empty;
    }
}
