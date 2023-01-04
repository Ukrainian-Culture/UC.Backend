using System.Net;
using Contracts;
using Entities;
using Entities.ErrorModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repositories;
using Ukranian_Culture.Backend;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), @"\nlog.config"));
builder.Services.AddScoped<ILoggerManager, LoggerManager>();
builder.Services.AddAutoMapper(typeof(Program));

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

app.UseExceptionHandler(appError =>
    appError.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature is not null)
        {
            ILoggerManager logger = new LoggerManager();
            logger.LogError($"Something went wrong: {contextFeature.Error}");

            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal server error"
            }.ToString());
        }
    }));

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



