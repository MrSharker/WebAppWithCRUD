using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebAppWithCRUD.Repositories;
using WebAppWithCRUD.Repositories.Contexts;
using WebAppWithCRUD.Repositories.Interfaces;
using WebAppWithCRUD.Services;
using WebAppWithCRUD.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProjectDbContext>(options =>
{
    options.UseSqlite("Data Source=Project.db");
});

builder.Services.AddScoped<IProjectDbContext>(provider =>
    provider.GetService<ProjectDbContext>());

builder.Services.AddScoped<IClientRepository,ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

using ( var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<ProjectDbContext>();
        Initialize(context);
    }
    catch (Exception exception)
    {
        var logger = serviceProvider.GetRequiredService<ILogger>();
        logger.LogError(exception, "An error occurred while app initialization");
    }
}

app.Run();


static void Initialize(ProjectDbContext context)
{
    context.Database.EnsureCreated();
}