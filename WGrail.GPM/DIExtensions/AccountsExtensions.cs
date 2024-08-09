namespace Wrail.GPM.FE.MVC.DIExtensions;

public static class AccountsExtensions
{
    public static IServiceCollection AddAccountsServices(this IServiceCollection services)
    {
        _ = services.AddScoped<Wrail.GPM.Accounts.Service.ICollection, Wrail.GPM.Accounts.Collection>();
        _ = services.AddScoped<Wrail.GPM.Accounts.Service.IAccount, Wrail.GPM.Accounts.Account>();
        _ = services.AddScoped<Wrail.GPM.Accounts.Service.Data.IQuery, Wrail.GPM.Accounts.Data.Query>();
        //_ = services.AddScoped<Wrail.GPM.Accounts.Service.Data.ICommand, Wrail.GPM.Accounts.Data.Command>();
        return services;
    }
}