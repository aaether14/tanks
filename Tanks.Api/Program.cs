using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tanks.Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();

    builder.Services.AddApplication();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.Run();
}

