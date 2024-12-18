using Backend.Config;
using Backend.Data.DTOs.Vehicle;
using Backend.Services.CompanyServices;
using Backend.Services.VehicleServices;
using FluentValidation;

namespace Backend.Endpoints;

public static class VehiclesEndpoints
{
    public static void MapVehiclesEndpoints(this WebApplication app)
    {
        var endpoint = app.MapGroup("api/vehicles")
            .WithOpenApi().WithTags("Vehicles");

        //Get All Vehicles
        //GET /api/vehicles
        endpoint.MapGet("", async (IVehicleServices vehicleService) =>
        {
            var response = new ApiResponse();

            try
            {
                var vehicles = await vehicleService.GetAllVehiclesAsync();

                response.Data = vehicles;
                response.IsSuccess = true;

                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");

                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetVehicles").WithSummary("Get All Vehicles");

        //Create New Vehicle
        //POST /api/vehicles
        endpoint.MapPost("", async (IVehicleServices vehicleService,
            ICompanyServices companyService,
            IValidator<UpsertVehicle> validator,
            UpsertVehicle data) =>
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

                var existCompany = await companyService.GetCompanyByIdAsync(data.CompanyId);

                if (existCompany is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الشركة غير موجودة");

                    return Results.Json(response, statusCode: 400);
                }

                var newVehicle = await vehicleService.AddVehicleAsync(data);

                if (newVehicle is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("مركبة بنفس رقم اللوحة موجودة");

                    return Results.Json(response, statusCode: 409);
                }

                response.Data = newVehicle;
                response.IsSuccess = true;

                return Results.Json(response, statusCode: 201);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");

                return Results.Json(response, statusCode: 500);
            }
        }).WithName("CreateVehicle").WithSummary("Create New Vehicle");

        //Get Single Vehicle By id
        //GET /api/vehicles/:id
        endpoint.MapGet("{id:int}", async (IVehicleServices vehicleService, int id) =>
        {
            var response = new ApiResponse();

            try
            {
                var vehicle = await vehicleService.GetVehicleByIdAsync(id);

                if (vehicle is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("المركبة غير موجودة");

                    return Results.Json(response, statusCode: 404);
                }

                response.Data = vehicle;
                response.IsSuccess = true;

                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");

                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetVehicle").WithSummary("Get Vehicle By Id");

        //Update Single Vehicle By id
        //PUT /api/vehicles/:id
        endpoint.MapPut("{id:int}", async (int id, IVehicleServices vehicleService,
            ICompanyServices companyService,
            IValidator<UpsertVehicle> validator,
            UpsertVehicle data) =>
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

                var existCompany = await companyService.GetCompanyByIdAsync(data.CompanyId);

                if (existCompany is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الشركة غير موجودة");

                    return Results.Json(response, statusCode: 400);
                }

                var updatedVehicle = await vehicleService.UpdateVehicleAsync(id, data);

                if (updatedVehicle is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("المركبة غير موجودة");

                    return Results.Json(response, statusCode: 404);
                }

                response.Data = updatedVehicle;
                response.IsSuccess = true;

                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");

                return Results.Json(response, statusCode: 500);
            }
        }).WithName("UpdateVehicle").WithSummary("Update Vehicle By Id");

        //Delete Single Vehicle By id
        //DELETE /api/vehicles/:id
        endpoint.MapDelete("{id:int}", async (int id, IVehicleServices vehicleService) =>
        {
            var response = new ApiResponse();

            try
            {
                var affRows = await vehicleService.DeleteVehicleAsync(id);

                if (affRows == 0)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("المركبة غير موجودة");

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
        }).WithName("DeleteVehicle").WithSummary("Delete Vehicle By Id");
    }
}