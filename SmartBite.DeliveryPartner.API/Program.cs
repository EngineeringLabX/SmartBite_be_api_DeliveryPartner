using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SmartBite.DeliveryPartner.Application.Interfaces;
using SmartBite.DeliveryPartner.Application.Mappers;
using SmartBite.DeliveryPartner.Application.Services;
using SmartBite.DeliveryPartner.Domain.Interfaces;
using SmartBite.DeliveryPartner.Infrastructure.Persistance;
using SmartBite.DeliveryPartner.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DeliveryPartnerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeliveryPartnerDbConnection")));

builder.Services.AddAutoMapper(cfg => { }, typeof(DeliveryPartnerProfile).Assembly);

builder.Services.AddScoped<IDeliveryPartnerRepository, DeliveryPartnerRepository>();

builder.Services.AddScoped<IDeliveryPartnerService, DeliveryPartnerService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
