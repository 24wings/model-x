using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;
using ModelX.Data;
using ModelX.Services;
using MudBlazor.Services;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
builder.Services.AddSyncfusionBlazor();
var dir = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
Console.WriteLine(dir);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

//builder.Services.AddDbContextFactory<AppDbContext>(opt => opt.UseSqlite($"Data Source=D:/csharp/app.db"));
builder.Services.AddDbContextFactory<AppDbContext>(opt => opt.UseMySql($"Server=10.1.7.75;Database=test;Uid=root;Pwd=123456;", serverVersion));



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddScoped<DatabaseQueryService>();


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