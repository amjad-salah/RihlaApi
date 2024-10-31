using Backend.Data.DTOs.Company;
using FluentValidation;

namespace Backend.Validations;

public class CompanyValidation : AbstractValidator<UpsertCompany>
{
    public CompanyValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("الإسم مطلوب")
            .NotNull().WithMessage("الإسم مطلوب")
            .MaximumLength(200).WithMessage("عدد حروف الإسم لايجب أن تتعدى ال 200 حرف");
        
        RuleFor(c => c.CountryId)
            .NotEmpty().WithMessage("الرجاء إختيار الدولة")
            .NotNull().WithMessage("الرجاء إختيار الدولة");
        
        RuleFor(c => c.Address)
            .NotEmpty().WithMessage("العنوان مطلوب")
            .NotNull().WithMessage("العنوان مطلوب")
            .MaximumLength(200).WithMessage("عدد حروف العنوان لايجب أن تتعدى ال 200 حرف");
        
        RuleFor(c => c.PhoneNo)
            .NotEmpty().WithMessage("رقم الهاتف مطلوب")
            .NotNull().WithMessage("رقم الهاتف مطلوب")
            .MaximumLength(15).WithMessage("عدد حروف رقم الهاتف لايجب أن تتعدى ال 15 حرف");
    }
}