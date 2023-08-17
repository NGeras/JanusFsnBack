using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Janus.DAL;

public class JanusDbContextFactory : IDesignTimeDbContextFactory<JanusDbContext>
{
    public JanusDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("designsettings.json")
            .Build();

        ServiceCollection services = new();

        services.AddDbContext<JanusDbContext>(
            options => options.UseSqlite(configuration.GetConnectionString("janusDB")), ServiceLifetime.Transient);

        var serviceProvider = services.BuildServiceProvider();

        return ActivatorUtilities.CreateInstance<JanusDbContext>(serviceProvider);
    }
}