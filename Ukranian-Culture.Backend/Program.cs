using Entities;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

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
builder.Services.AddDbContext<RepositoryContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

var services = (IServiceScopeFactory)app.Services.GetService(typeof(IServiceScopeFactory))!;

using (var db = services.CreateScope().ServiceProvider.GetService<RepositoryContext>())
{
    db!.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
