using System.Text.Json.Serialization;
using Backend.Data.Context;
using Backend.Data.DTOs.City;
using Backend.Data.DTOs.Company;
using Backend.Data.DTOs.Country;
using Backend.Data.DTOs.Driver;
using Backend.Data.DTOs.Journey;
using Backend.Data.DTOs.Vehicle;
using Backend.Endpoints;
using Backend.Services.CityServices;
using Backend.Services.CompanyServices;
using Backend.Services.CountryServices;
using Backend.Services.DriverServices;
using Backend.Services.JourneyServices;
using Backend.Services.VehicleServices;
using Backend.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JSON Handler
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});


//Context
builder.Services.AddDbContext<RihlaContext>(o => o.UseSqlite(connectionString));

//Validations
builder.Services.AddScoped<IValidator<UpsertCountry>, CountryValidation>();
builder.Services.AddScoped<IValidator<UpsertCompany>, CompanyValidation>();
builder.Services.AddScoped<IValidator<UpsertCity>, CityValidation>();
builder.Services.AddScoped<IValidator<UpsertDriver>, DriverValidation>();
builder.Services.AddScoped<IValidator<UpsertVehicle>, VehicleValidation>();
builder.Services.AddScoped<IValidator<UpsertJourney>, JourneyValidation>();

//Services
builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<ICompanyServices, CompanyServices>();
builder.Services.AddScoped<ICityServices, CityServices>();
builder.Services.AddScoped<IDriverServices, DriverServices>();
builder.Services.AddScoped<IVehicleServices, VehicleServices>();
builder.Services.AddScoped<IJourneyServices, JourneyServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapCountriesEndpoints();
app.MapCompaniesEndpoints();
app.MapCitiesEndpoints();
app.MapDriversEndpoints();
app.MapVehiclesEndpoints();
app.MapJourneysEndpoints();

app.Run();