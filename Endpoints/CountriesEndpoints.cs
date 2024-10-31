using Backend.Config;
using Backend.Data.DTOs.Country;
using Backend.Services.CountryServices;
using FluentValidation;

namespace Backend.Endpoints;

public static class CountriesEndpoints
{
    public static void MapCountriesEndpoints(this WebApplication app)
    {
        var endpoint = app.MapGroup("api/countries").WithOpenApi()
            .WithTags("Countries");
        
        //Get All Countries
        //GET /api/countries
        endpoint.MapGet("", async (ICountryServices db) =>
        {
            var response = new ApiResponse();
            
            try
            {
                var countries = await db.GetAllCountriesAsync();
                
                response.IsSuccess = true;
                response.Data = countries;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetCountries")
        .WithSummary("Get All Countries");
        
        //Create New Country
        //POST /api/countries
        endpoint.MapPost("", async (IValidator<UpsertCountry> validator, ICountryServices db, UpsertCountry data) =>
        {
            var response = new ApiResponse();

            try
            {
                var validationResult = await validator.ValidateAsync(data);

                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    foreach (var error in validationResult.Errors)
                    {
                        response.Errors.Add(error.ErrorMessage);
                    }
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                var newCountry = await db.AddCountryAsync(data);

                if (newCountry is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("هذه الدولة موجودة");
                    
                    return Results.Json(response, statusCode: 409);
                }
                
                response.IsSuccess = true;
                response.Data = newCountry;
                
                return Results.Json(response, statusCode: 201);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("CreateCountry")
        .WithSummary("Create New Country");
        
        //Get Single Country by id
        //GET /api/countries/:id
        endpoint.MapGet("{id:int}", async (int id, ICountryServices db) =>
        {
            var response = new ApiResponse();

            try
            {
                var company = await db.GetCountryByIdAsync(id);

                if (company is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الدولة غير موجودة");
                    
                    return Results.Json(response, statusCode: 404);
                }
                
                response.IsSuccess = true;
                response.Data = company;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetCountry")
        .WithSummary("Get Country By Id");
        
        //Update Single Country by id
        //Put /api/countries/:id
        endpoint.MapPut("{id:int}", async (int id,IValidator<UpsertCountry> validator, ICountryServices db, UpsertCountry data) =>
        {
            var response = new ApiResponse();

            try
            {
                var validationResult = await validator.ValidateAsync(data);

                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    foreach (var error in validationResult.Errors)
                    {
                        response.Errors.Add(error.ErrorMessage);
                    }
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                var updatedCountry = await db.UpdateCountryAsync(id, data);

                if (updatedCountry is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الدولة غير موجودة");
                    
                    return Results.Json(response, statusCode: 404);
                }
                
                response.IsSuccess = true;
                response.Data = updatedCountry;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("UpdateCountry")
        .WithSummary("Update Country By Id");
        
        //Delete Single Country by id
        //Delete /api/countries/:id
        endpoint.MapDelete("{id:int}", async (int id, ICountryServices db) =>
        {
            var response = new ApiResponse();

            try
            {
                var affRows = await db.DeleteCountryAsync(id);

                if (affRows == 0)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الدولة غير موجودة");
                    
                    return Results.Json(response, statusCode: 404);
                }
                
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("DeleteCountry")
        .WithSummary("Delete Country By Id");
    }
}