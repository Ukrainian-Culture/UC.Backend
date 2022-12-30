using Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RepositoryContext>(
    opts => opts.UseSqlServer(
        builder.Configuration.GetConnectionString("Db_connection"),
        b => b.MigrationsAssembly("Ukranian-Culture.Backend")
    )
);

var app = builder.Build();
var logger = NLog.LogManager.GetCurrentClassLogger();

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



