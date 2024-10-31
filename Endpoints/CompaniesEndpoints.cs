using Backend.Config;
using Backend.Data.DTOs.Company;
using Backend.Services.CompanyServices;
using FluentValidation;

namespace Backend.Endpoints;

public static class CompaniesEndpoints
{
    public static void MapCompaniesEndpoints(this WebApplication app)
    {
        var endpoint = app.MapGroup("api/companies").WithOpenApi()
            .WithTags("Companies");
        
        //Get All Companies
        //GET /api/companies
        endpoint.MapGet("", async (ICompanyServices db) =>
        {
            var response = new ApiResponse();
            try
            {
                var companies = await db.GetAllCompaniesAsync();
                
                response.Data = companies;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetCompanies")
        .WithSummary("Get All Companies");
        
        //Create New Company
        //POST /api/companies
        endpoint.MapPost("", async (IValidator<UpsertCompany> validator, 
            ICompanyServices db, UpsertCompany data) =>
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
                
                var newCompany = await db.AddCompanyAsync(data);

                if (newCompany is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("هذه الشركة موجودة");
                    
                    return Results.Json(response, statusCode: 409);
                }
                
                response.Data = newCompany;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 201);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("AddCompany")
        .WithSummary("Add New Company");
        
        //Get Single Company By id
        //GET /api/companies/:id
        endpoint.MapGet("{id:int}", async (int id, ICompanyServices db) =>
        {
            var response = new ApiResponse();

            try
            {
                var company = await db.GetCompanyByIdAsync(id);

                if (company is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الشركة غير موجودة");
                    
                    return Results.Json(response, statusCode: 404);
                }
                
                response.Data = company;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("GetCompany")
        .WithSummary("Get Company By Id");
        
        //Update Single Company By id
        //PUT /api/companies/:id
        endpoint.MapPut("{id:int}", async (int id,IValidator<UpsertCompany> validator,
            ICompanyServices db, UpsertCompany data) =>
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
                
                var updatedCompany = await db.UpdateCompanyAsync(id, data);

                if (updatedCompany is null)
                {
                    response.IsSuccess = false;
                    response.Errors.Add("الشركة غير موجودة");
                    
                    return Results.Json(response, statusCode: 404);
                }
                
                response.Data = updatedCompany;
                response.IsSuccess = true;
                
                return Results.Json(response, statusCode: 200);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Errors.Add("خطأ في المخدم، الرجاء المحاولة لاحقاً");
                
                return Results.Json(response, statusCode: 500);
            }
        }).WithName("UpdateCompany")
        .WithSummary("Update Company By Id");
        
        //Delete Single Company By id
        //DELETE /api/companies/:id
        endpoint.MapDelete("{id:int}", async (int id, ICompanyServices db) =>
        {
            var response = new ApiResponse();

            try
            {
                var affRows = await db.DeleteCompanyAsync(id);

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
        }).WithName("DeleteCompany")
        .WithSummary("Delete Company By Id");
    }
}