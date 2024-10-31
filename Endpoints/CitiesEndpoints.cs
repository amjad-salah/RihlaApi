using Backend.Config;
using Backend.Data.DTOs.City;
using Backend.Services.CityServices;
using FluentValidation;

namespace Backend.Endpoints;

public static class CitiesEndpoints
{
    public static void MapCitiesEndpoints(this WebApplication app)
    {
        var endpoint = app.MapGroup("api/cities").WithOpenApi()
            .WithTags("Cities");
        
        //Get All Cities
        //GET /api/cities
        endpoint.MapGet("", async (ICityServices db) =>
        {
            var response = new ApiResponse();

            try
            {
                var cities = await db.GetAllCitiesAsync();
                
                response.Data = cities;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetCities").WithSummary("Get All Cities");
        
        //Create New City
        //POST /api/cities
        endpoint.MapPost("", async (ICityServices db,
            IValidator<UpsertCity> validator,
            UpsertCity data) =>
        {
            var response = new ApiResponse();

            try
            {
                var validate = await validator.ValidateAsync(data);

                if (!validate.IsValid)
                {
                    response.IsSuccess = false;

                    foreach (var error in validate.Errors)
                    {
                        response.Errors.Add(error.ErrorMessage);
                    }
                    
                    return Results.Json(response, statusCode: 400);
                }

                var newCity = await db.AddCityAsync(data);

                if (newCity is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("المدينة موجودة");
                    
                    return Results.Json(response, statusCode: 409);
                }
                
                response.IsSuccess = true;
                response.Data = newCity;
                
                return Results.Json(response, statusCode: 201);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("AddCity").WithSummary("Create New City");
        
        //Get Single City By id
        //GET /api/cities/:id
        endpoint.MapGet("{id:int}", async (int id, ICityServices db) =>
        {
            var response = new ApiResponse();

            try
            {
                var city = await db.GetCityByIdAsync(id);

                if (city is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الشركة غير موجودة");
                    
                    return Results.Json(response, statusCode: 404);
                }
                
                response.IsSuccess = true;
                response.Data = city;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetCity").WithSummary("Get City By Id");
        
        //Update Single City By id
        //PUT /api/cities/:id
        endpoint.MapPut("{id:int}", async (int id, ICityServices db,
            IValidator<UpsertCity> validator,
            UpsertCity data) =>
        {
            var response = new ApiResponse();

            try
            {
                var validate = await validator.ValidateAsync(data);

                if (!validate.IsValid)
                {
                    response.IsSuccess = false;

                    foreach (var err in validate.Errors)
                    {
                        response.Errors.Add(err.ErrorMessage);
                    }
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                var updateCity = await db.UpdateCityAsync(id, data);

                if (updateCity is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("المدينة غير موجودة");
                    
                    return Results.Json(response, statusCode: 404);
                }
                
                response.IsSuccess = true;
                response.Data = updateCity;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("UpdateCity").WithSummary("Update City By Id");
        
        //Delete Single City By id
        //DELETE /api/cities/:id
        endpoint.MapDelete("{id:int}", async (int id, ICityServices db) =>
        {
            var response = new ApiResponse();

            try
            {
                var affRows = await db.DeleteCityAsync(id);

                if (affRows == 0)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الشركة غير موجودة");
                    
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
        }).WithName("DeleteCity").WithSummary("Delete City By Id");
    }
}