using Microsoft.EntityFrameworkCore;
using Acorn.Domain.Entities.Post;

namespace Acorn.Infrastructure;

public class MySqlContext : DbContext
{
    private readonly IConfiguration configuration;

    public MySqlContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            //.UseLazyLoadingProxies()
            .UseMySql(configuration.GetConnectionString("Database"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("Database")));

#if DEBUG
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
        options.LogTo(Console.WriteLine);
#endif
    }
}
