using BackendApp.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySql");
var dbServerVersion = new MySqlServerVersion(new Version(8, 0, 32));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "app/dist";
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, dbServerVersion);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseRouting();

app.UseEndpoints(routes =>
{
    routes.MapControllers();
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "app";

    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
    }
});

app.Run();
