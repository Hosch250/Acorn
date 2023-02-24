using Acorn.Application;
using Acorn.Domain;
using Acorn.Domain.Entities.Category;
using Acorn.Domain.Entities.Community;
using Acorn.Domain.Entities.Post;
using Acorn.Domain.Entities.Tag;
using Acorn.Domain.Entities.User;
using Acorn.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Not a huge fan of this, but MediatR needs it to resolve the DB context...
builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Acorn", Version = "v1" }));

builder.Services.AddDbContext<IDbContext, MySqlContext>();
builder.Services.AddDbContext<IGlobalDbContext, GlobalMySqlContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// todo: pull this out into an `AddIdentity` call
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<GlobalMySqlContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<User, GlobalMySqlContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Authenticated", policy =>
    {
        policy.RequireAuthenticatedUser();
    });

    // todo: rename
    // treating "Tier1" as the first tier of permissions granted, based on earned rep
    // we can also add policies based on other claims, etc., such as whether the user is question-banned
    options.AddPolicy("Tier1", policy =>
    {
        policy.RequireClaim("Tier1");
    });
});

// todo: pull these out into an `AddPosts`, etc, call
// or maybe an `AddLocalServices` call?
builder.Services.AddTransient<IPostApplication, PostApplication>();
builder.Services.AddTransient<PostFactory>();

builder.Services.AddTransient<ITagApplication, TagApplication>();
builder.Services.AddTransient<TagFactory>();

builder.Services.AddTransient<ICategoryApplication, CategoryApplication>();
builder.Services.AddTransient<CategoryFactory>();

builder.Services.AddTransient<ICommunityApplication, CommunityApplication>();
builder.Services.AddTransient<CommunityFactory>();

Assembly.GetExecutingAssembly().GetTypes()
    .Where(w => w.BaseType is { IsGenericType: true } &&
        w.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
    .ToList()
    .ForEach(f => builder.Services.AddTransient(f));

builder.Services.AddMediatR(a =>
{
    a.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddCors(o => o.AddPolicy("default", p =>
{
    p.WithOrigins("http://localhost:3000");
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Acorn v1"));
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("default");

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapFallbackToFile("index.html");

var serviceProvider = app.Services;
DomainEvents.Publisher = serviceProvider.GetRequiredService<IPublisher>;

app.Run();
