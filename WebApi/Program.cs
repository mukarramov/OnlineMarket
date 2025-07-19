using FluentValidation;
using Infrastructure.ApplicationDbContext;
using Infrastructure.Interceptors;
using Infrastructure.Validations;
using IT_RunCourseSecondPartAPI.Extensions;
using IT_RunCourseSecondPartAPI.Mapper;
using Microsoft.EntityFrameworkCore;
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
            options.UseMySQL(databaseConnectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .AddInterceptors(sp.GetRequiredService<SaveChangeInterceptor>());
        });


        builder.Services.DependInjection();

        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(x => { x.AddMaps(typeof(MapperProfile).Assembly); });

        builder.Services.AddValidatorsFromAssemblyContaining<UserCreateValidation>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}