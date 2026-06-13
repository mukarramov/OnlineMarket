using System.Text;
using FluentValidation;
using Infrastructure.ApplicationDbContext;
using Infrastructure.Interceptors;
using Infrastructure.Validations;
using IT_RunCourseSecondPartAPI.Extensions;
using IT_RunCourseSecondPartAPI.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;

namespace IT_RunCourseSecondPartAPI;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.Debug().CreateLogger();

        Log.Information("the web app started!");

        var builder = WebApplication.CreateBuilder(args);

        builder.Host.ConfigureSerilog();

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

        builder.Services.AddOpenApi();

        builder.AddServiceDefaults();

        var databaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseNpgsql(databaseConnectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .AddInterceptors(sp.GetRequiredService<SaveChangeInterceptor>());
        });

        builder.Services.DependInjection();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAutoMapper(x => { x.AddMaps(typeof(MapperProfile).Assembly); });

        builder.Services.AddValidatorsFromAssemblyContaining<UserCreateValidation>();

        builder.Services.AddSwaggerGen(x =>
        {
            x.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });
            x.AddSecurityRequirement(
                _ => new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("Bearer"),
                        new List<string>()
                    }
                });
        });

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException()))
            };
        });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("AllRoles", policy => policy.RequireRole("SuperAdmin", "Client", "Admin"))
            .AddPolicy("OnlyAdmin", policy => policy.RequireRole("SuperAdmin"))
            .AddPolicy("OnlyAdmins", policy => policy.RequireRole("SuperAdmin", "Admin"))
            .AddPolicy("OnlyClient", policy => policy.RequireRole("Client"));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
