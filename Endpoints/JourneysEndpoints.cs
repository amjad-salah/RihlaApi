using Backend.Config;
using Backend.Data.DTOs.Journey;
using Backend.Services.CityServices;
using Backend.Services.CompanyServices;
using Backend.Services.DriverServices;
using Backend.Services.JourneyServices;
using Backend.Services.VehicleServices;
using FluentValidation;

namespace Backend.Endpoints;

public static class JourneysEndpoints
{
    public static void MapJourneysEndpoints(this WebApplication app)
    {
        var endpoint = app.MapGroup("api/journeys")
                        .WithOpenApi().WithTags("Journeys");

        //Get All Journeys
        //GET /api/journeys
        endpoint.MapGet("", async (IJourneyServices journeyService) =>
        {
            var response = new ApiResponse();

            try
            {
                var journeys = await journeyService.GetAllJourneysAsync();
                
                response.Data = journeys;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");

                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetJourneys").WithSummary("Get All Journeys");
        
        //Create New Journey
        //POST /api/journeys
        endpoint.MapPost("", async (IJourneyServices journeyService,
            ICompanyServices companyService,
            IDriverServices driverService,
            IVehicleServices vehicleService,
            ICityServices cityService,
            IValidator<UpsertJourney> validator,
            UpsertJourney data) =>
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
                
                var existDriver = await driverService.GetDriverByIdAsync(data.DriverId);

                if (existDriver is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("السائق غير موجود");
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                var existVehicle = await vehicleService.GetVehicleByIdAsync(data.VehicleId);

                if (existVehicle is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("المركبة غير موجودة");
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                var existDepCity = await cityService.GetCityByIdAsync(data.DepCityId);

                if (existDepCity is not null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("جهة القيام غير موجودة");
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                var existArrCity = await cityService.GetCityByIdAsync(data.ArrCityId);

                if (existArrCity is not null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("جهة الوصول غير موجودة");
                    
                    return Results.Json(response, statusCode: 400);
                }

                var newJourney = await journeyService.AddJourneyAsync(data);

                if (newJourney is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("السائق/ المركبة غير متوفر/ة خلال هذه الفترة");
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                response.Data = newJourney;
                response.IsSuccess = true;
                
                return Results.Json(newJourney, statusCode: 201);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");

                return Results.Json(response, statusCode: 500);
            }
        }).WithName("AddJourney").WithSummary("Add New Journey");
        
        //Get Single Journey By id
        //GET /api/journeys/:id
        endpoint.MapGet("{id:int}", async (int id, IJourneyServices journeyService) =>
        {
            var response = new ApiResponse();

            try
            {
                var journey = await journeyService.GetJourneyByIdAsync(id);

                if (journey is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الرحلة غير موجودة");
                    
                    return Results.Json(response, statusCode: 404);
                }
                
                response.Data = journey;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");

                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetJourney").WithSummary("Get Journey By Id");
        
        //Update Single Journey By id
        //PUT /api/journeys/:id
        endpoint.MapPut("{id:int}", async (int id, IJourneyServices journeyService,
            ICompanyServices companyService,
            IDriverServices driverService,
            IVehicleServices vehicleService,
            ICityServices cityService,
            IValidator<UpsertJourney> validator,
            UpsertJourney data) =>
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
                
                var existDriver = await driverService.GetDriverByIdAsync(data.DriverId);

                if (existDriver is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("السائق غير موجود");
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                var existVehicle = await vehicleService.GetVehicleByIdAsync(data.VehicleId);

                if (existVehicle is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("المركبة غير موجودة");
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                var existDepCity = await cityService.GetCityByIdAsync(data.DepCityId);

                if (existDepCity is not null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("جهة القيام غير موجودة");
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                var existArrCity = await cityService.GetCityByIdAsync(data.ArrCityId);

                if (existArrCity is not null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("جهة الوصول غير موجودة");
                    
                    return Results.Json(response, statusCode: 400);
                }

                var updatedJourney = await journeyService.UpdateJourneyAsync(id, data);

                if (updatedJourney is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("السائق/ المركبة غير متوفر/ة خلال هذه الفترة، أو الرحلة غير موجودة");
                    
                    return Results.Json(response, statusCode: 400);
                }
                
                response.Data = updatedJourney;
                response.IsSuccess = true;
                
                return Results.Json(updatedJourney, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");

                return Results.Json(response, statusCode: 500);
            }
        }).WithName("UpdateJourney").WithSummary("Update Journey By Id");
        
        //Delete Single Journey By id
        //DELETE /api/journeys/:id
        endpoint.MapDelete("{id:int}", async (int id, IJourneyServices journeyService) =>
        {
            var response = new ApiResponse();

            try
            {
                var affRows = await journeyService.DeleteJourneyAsync(id);

                if (affRows == 0)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الرحلة غير موجودة");
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
        });
    }
}
