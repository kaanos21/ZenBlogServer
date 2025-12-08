using Scalar.AspNetCore;
using ZenBlog.API.CustomMiddlewares;
using ZenBlog.API.Endpoints.Registration;
using ZenBlog.Application.Extensions;
using ZenBlog.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<CustomExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGroup("/api")
    .RegisterEndpoints();

app.Run();
