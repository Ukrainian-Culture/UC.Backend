using System.Net;
using System.Text;
using Contracts;
using Entities;
using Entities.ErrorModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using Repositories;
using Parsers;
using Entities.Configurations;
using Lucene.Net.Util;
using Microsoft.OpenApi.Models;
using Ukranian_Culture.Backend.ActionFilters.ArticleLocaleActionFilters;
using Ukranian_Culture.Backend.Services;
using OnlineUsersHub = Ukranian_Culture.Backend.Services.OnlineUsersHub;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), @"\nlog.config"));
builder.Services.AddScoped<ILoggerManager, LoggerManager>();
builder.Services.AddTransient<IParser, Parser>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddMvc();
builder.Services.AddScoped<IErrorMessageProvider, ErrorMessageProvider>();
builder.Services.AddSignalR();
builder.Services.AddScoped<IArticleTileService, ArticleTilesService>();
builder.Services.Decorate<IArticleTileService, CachingArticleTileService>();
builder.Services.AddScoped<ArticleLocaleIEmumerableExistAttribute>();
builder.Services.AddScoped<ArticleLocaleExistAttribute>();
builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(provider => new Lazy<IUserRepository>(
    () => provider.GetService<IUserRepository>()!,
    LazyThreadSafetyMode.ExecutionAndPublication));

builder.Services.AddScoped<IArticleLocalesRepository, ArticleLocalesRepository>();
builder.Services.AddScoped(provider => new Lazy<IArticleLocalesRepository>(
    () => provider.GetService<IArticleLocalesRepository>()!,
    LazyThreadSafetyMode.ExecutionAndPublication));


builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped(provider => new Lazy<IArticleRepository>(
    () => provider.GetService<IArticleRepository>()!,
    LazyThreadSafetyMode.ExecutionAndPublication));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped(provider => new Lazy<ICategoryRepository>(
    () => provider.GetService<ICategoryRepository>()!,
    LazyThreadSafetyMode.ExecutionAndPublication));

builder.Services.AddScoped<ICultureRepository, CultureRepository>();
builder.Services.AddScoped(provider => new Lazy<ICultureRepository>(
    () => provider.GetService<ICultureRepository>()!,
    LazyThreadSafetyMode.ExecutionAndPublication));

builder.Services.AddScoped<IUserHistoryRepository, UserHistoryRepository>();
builder.Services.AddScoped(provider => new Lazy<IUserHistoryRepository>(
    () => provider.GetService<IUserHistoryRepository>()!,
    LazyThreadSafetyMode.ExecutionAndPublication));

builder.Services.AddTransient<IRepositoryManager, RepositoryManager>();

builder.Services.AddDbContext<RepositoryContext>(
    opts => opts.UseSqlServer(
        builder.Configuration.GetConnectionString("Db_connection")!,
        b => b.MigrationsAssembly("Ukranian-Culture.Backend")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddIdentity<User, Roles>(opts =>
{
    opts.Password.RequiredLength = 8;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
    opts.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<RepositoryContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(option =>
    {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });


builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.AddTransient<IMailing, Mailing>();

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

try
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseRouting();
    app.UseAuthorization();

    app.UseCors("CorsPolicy");
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<OnlineUsersHub>("/onlineUsersHuber");
    });
}
catch (Exception ex)
{
    logger.Error(ex, "Error!");
}
finally
{
    app.Run();
}



