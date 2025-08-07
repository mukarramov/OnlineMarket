using Infrastructure.ApplicationDbContext;
using IT_RunCourseSecondPartAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UserTest;

public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(service =>
        {
            var descriptor = service.SingleOrDefault(x => x.ServiceType ==
                                                          typeof(DbContextOptions<AppDbContext>));

            if (descriptor == null)
            {   
                service.Remove(descriptor);
            }

            const string databaseConnectionString =
                "Host=localhost;Port=5432;Database=TestD;Username=postgres;Password=Bm05";

            service.AddDbContext<AppDbContext>(options => options.UseNpgsql(databaseConnectionString));

            var sp = service.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        });
    }
}