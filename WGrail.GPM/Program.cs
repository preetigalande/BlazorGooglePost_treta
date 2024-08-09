using log4net.Config;
using log4net;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WGrail.GPM.Data;
using WGrail.GPM.FE.Blazor.Models;
using Wrail.GPM.FE.MVC.DIExtensions;
using Microsoft.Extensions.Options;
using Wrail.GPM.Database.SQLServer.Service;
using Wrail.GPM.Accounts.Data;
using Wrail.GPM.Accounts.Service.Data;
using Wrail.GPM.Accounts.Service;
using Wrail.GPM.Accounts.Service.Data.DTO;
using Wrail.GPM.Accounts;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
XmlConfigurator.Configure(new FileInfo("log4net.config"));

ILog log = LogManager.GetLogger(typeof(Program));
log.Info("Application started.");

//http service: adds an instance of the HttpClient
builder.Services.AddHttpClient();

//app settings - configuration options
//databases
//SQLServer connection string
builder.Services.Configure<Wrail.GPM.FE.MVC.AppSettings.Data.SQLServer.Options.DbConnectionString>(
    builder.Configuration.GetSection(Wrail.GPM.FE.MVC.AppSettings.Data.SQLServer.Options.DbConnectionString.ConfigPath)
);

// Register IConnectionString implementation
builder.Services.AddScoped<Wrail.GPM.Database.SQLServer.Service.IConnectionString>(sp =>
{
    var options = sp.GetRequiredService<IOptions<Wrail.GPM.FE.MVC.AppSettings.Data.SQLServer.Options.DbConnectionString>>().Value;
    return options;
});


// Register other services
builder.Services.AddHttpClient(); // Adds an HttpClient instance


builder.Services.AddDatabaseServices();
builder.Services.AddAccountsServices();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
