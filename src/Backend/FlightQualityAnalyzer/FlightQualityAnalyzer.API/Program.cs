// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FlightQualityAnalyzer.API.Middlewares;
using FlightQualityAnalyzer.Domain.DTOs;
using FlightQualityAnalyzer.Domain.Interfaces;
using FlightQualityAnalyzer.Infrastructure.Repository;
using FlightQualityAnalyzer.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<FlightFileSetting>(builder.Configuration.GetSection("FlightFileSetting"));
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IFlightService, FlightService>();

// these two lines to configure Global exception handler and problem detail
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
