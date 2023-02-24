using Acorn.Domain.Entities.Category;
using Acorn.Domain.Entities.Post;
using Acorn.Domain.Entities.Tag;
using Microsoft.EntityFrameworkCore;

namespace Acorn.Infrastructure;

public interface IDbContext
{
    public DbSet<Category> Category { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync() => SaveChangesAsync(CancellationToken.None);
}

public class MySqlContext : DbContext, IDbContext
{
    private readonly IConfiguration configuration;

    public MySqlContext(IConfiguration configuration, DbContextOptions<MySqlContext> contextOptions) : base(contextOptions)
    {
        this.configuration = configuration;
    }

    public DbSet<Category> Category { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

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