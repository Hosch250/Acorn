using Acorn.Application;
using Acorn.Domain;
using Acorn.Domain.Entities.Post;
using Acorn.Domain.Entities.Tag;
using Acorn.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Not a huge fan of this, but MediatR needs it to resolve the DB context...
builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Acorn", Version = "v1" }));

builder.Services.AddDbContext<MySqlContext>();

builder.Services.AddTransient<IPostApplication, PostApplication>();
builder.Services.AddTransient<PostFactory>();

builder.Services.AddTransient<ITagApplication, TagApplication>();
builder.Services.AddTransient<TagFactory>();

Assembly.GetExecutingAssembly().GetTypes()
    .Where(w => w.BaseType is { IsGenericType: true } &&
        w.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
    .ToList()
    .ForEach(f => builder.Services.AddTransient(f));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

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
DomainEvents.Publisher = serviceProvider.GetRequiredService<IPublisher>;

app.Run();
