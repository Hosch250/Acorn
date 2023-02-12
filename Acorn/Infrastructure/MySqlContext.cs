using Microsoft.EntityFrameworkCore;
using Acorn.Domain.Entities.Post;
using Acorn.Domain.Entities.Tag;

namespace Acorn.Infrastructure;

public class MySqlContext : DbContext
{
    private readonly IConfiguration configuration;

    public MySqlContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseMySql(configuration.GetConnectionString("Database"),
            ServerVersion.AutoDetect(configuration.GetConnectionString("Database")));

#if DEBUG
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
        options.LogTo(Console.WriteLine);
#endif
    }
}
