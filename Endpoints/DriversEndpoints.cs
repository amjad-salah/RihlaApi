using System.ComponentModel.DataAnnotations;
using Backend.Config;
using Backend.Data.Context;
using Backend.Data.DTOs.Driver;
using Backend.Services.DriverServices;
using FluentValidation;

namespace Backend.Endpoints;

public static class DriversEndpoints
{
    public static void MapDriversEndpoints(this WebApplication app)
    {
        var endpoint = app.MapGroup("api/drivers")
            .WithOpenApi().WithTags("Drivers");
        
        //Get All Drivers
        //GET /api/drivers
        endpoint.MapGet("", async (IDriverServices db) =>
        {
            var response = new ApiResponse();

            try
            {
                var drivers = await db.GetAllDriversAsync();
                
                response.Data = drivers;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetDrivers").WithSummary("Get All Drivers");
        
        //Create New Driver
        //POST /api/drivers
        endpoint.MapPost("", async (IDriverServices db,
            IValidator<UpsertDriver> validator,
            UpsertDriver data) =>
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

                var newDriver = await db.AddDriverAsync(data);

                if (newDriver is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("سائق بنفس رقم الرخصة موجود");
                    
                    return Results.Json(response, statusCode: 409);
                }
                
                response.Data = newDriver;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 201);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("AddDriver").WithSummary("Add New Driver");
        
        //Get Single Driver By id
        //GET /api/drivers/:id
        endpoint.MapGet("{id:int}", async (int id, IDriverServices db) =>
        {
            var response = new ApiResponse();

            try
            {
                var driver = await db.GetDriverByIdAsync(id);

                if (driver is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("السائق غير موجودة");
                    
                    return Results.Json(response, statusCode: 404);
                }
                
                response.Data = driver;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetDriver").WithSummary("Get Driver By Id");
        
        //Update Single Driver By id
        //PUT /api/drivers/:id
        endpoint.MapPut("{id:int}", async (int id, IDriverServices db,
            UpsertDriver data,
            IValidator<UpsertDriver> validator) =>
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
                
                var updateDriver = await db.UpdateDriverAsync(id, data);

                if (updateDriver is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("السائق غير موجود");
                    
                    return Results.Json(response, statusCode: 404);
                }
                
                response.Data = updateDriver;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("UpdateDriver").WithSummary("Update Driver By Id");
        
        //Delete Single Driver By id
        //DELETE /api/drivers/:id
        endpoint.MapDelete("{id:int}", async (int id, IDriverServices db) =>
        {
            var response = new ApiResponse();

            try
            {
                var affRows = await db.DeleteDriverAsync(id);

                if (affRows == 0)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("السائق غير موجود");
                    
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
        }).WithName("DeleteDriver").WithSummary("Delete Driver By Id");
    }
}