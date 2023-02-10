using Acorn.Application;
using Acorn.Domain;
using Acorn.Domain.Entities.Post;
using Acorn.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Acorn", Version = "v1" }));

builder.Services.AddDbContext<MySqlContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(builder.Configuration.GetConnectionString("Database"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Database")))
#if DEBUG
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
#endif
);

builder.Services.AddTransient<IPostApplication, PostApplication>();
builder.Services.AddTransient<PostFactory>();

Assembly.GetExecutingAssembly().GetTypes()
    .Where(w => w.BaseType is { IsGenericType: true } &&
        w.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
    .ToList()
    .ForEach(f => builder.Services.AddTransient(f));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Acorn v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var serviceProvider = app.Services;
DomainEvents.Publisher = () => serviceProvider.GetRequiredService<IPublisher>();

app.Run();
