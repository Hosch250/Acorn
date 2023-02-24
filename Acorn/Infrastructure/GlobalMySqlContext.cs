using Acorn.Domain.Entities.Community;
using Acorn.Domain.Entities.User;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Acorn.Infrastructure;

// auth system designed from https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?source=recommendations&view=aspnetcore-7.0
public interface IGlobalDbContext : IPersistedGrantDbContext
{
    public DbSet<Community> Communities { get; set; }
    public DbSet<User> Users { get; set; }
}

public class GlobalMySqlContext : ApiAuthorizationDbContext<User>, IGlobalDbContext
{
    private readonly IConfiguration configuration;

    public GlobalMySqlContext(IConfiguration configuration, DbContextOptions<GlobalMySqlContext> contextOptions, IOptions<OperationalStoreOptions> options) : base(contextOptions, options)
    {
        this.configuration = configuration;
    }

    public DbSet<Community> Communities { get; set; }
    new public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseMySql(configuration.GetConnectionString("GlobalDatabase"),
            ServerVersion.AutoDetect(configuration.GetConnectionString("GlobalDatabase")));

#if DEBUG
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
        options.LogTo(Console.WriteLine);
#endif
    }
}
