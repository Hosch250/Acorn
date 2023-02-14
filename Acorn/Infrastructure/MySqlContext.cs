using Microsoft.EntityFrameworkCore;
using Acorn.Domain.Entities.Post;
using Acorn.Domain.Entities.Tag;
using Acorn.Domain.Entities.User;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;

namespace Acorn.Infrastructure;

// auth system designed from https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?source=recommendations&view=aspnetcore-7.0
public interface IDbContext : IPersistedGrantDbContext
{
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }

    new Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    new Task<int> SaveChangesAsync() => SaveChangesAsync(CancellationToken.None);
}

public class MySqlContext : ApiAuthorizationDbContext<User>, IDbContext
{
    private readonly IConfiguration configuration;

    public MySqlContext(IConfiguration configuration, DbContextOptions contextOptions, IOptions<OperationalStoreOptions> options) : base(contextOptions, options)
    {
        this.configuration = configuration;
    }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PersistedGrant> PersistedGrants { get; set; }
    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
    public DbSet<Key> Keys { get; set; }

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
