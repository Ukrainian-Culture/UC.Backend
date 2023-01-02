using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repositories;
using Ukranian_Culture.Backend;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), @"\nlog.config"));
builder.Services.AddScoped<ILoggerManager, LoggerManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RepositoryContext>(
    opts => opts.UseSqlServer(
        builder.Configuration.GetConnectionString("Db_connection")!,
        b => b.MigrationsAssembly("Ukranian-Culture.Backend")
    )
);
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
var app = builder.Build();
var logger = LogManager.GetCurrentClassLogger();

var services = (IServiceScopeFactory)app.Services.GetService(typeof(IServiceScopeFactory))!;
using (var db = services.CreateScope().ServiceProvider.GetService<RepositoryContext>())
{
    db!.Database.Migrate();
}

try
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}
catch (Exception ex)
{
    logger.Error(ex,"Error!");
}
finally
{
    app.Run();
}



