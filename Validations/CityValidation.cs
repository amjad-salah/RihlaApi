using Backend.Data.DTOs.City;
using FluentValidation;

namespace Backend.Validations;

public class CityValidation : AbstractValidator<UpsertCity>
{
    public CityValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("الإسم مطلوب")
            .NotNull().WithMessage("الإسم مطلوب")
            .MaximumLength(50).WithMessage("عدد حروف الإسم لايجب أن تتعدى ال 50 حرف");
        
        RuleFor(c => c.CountryId)
            .NotEmpty().WithMessage("الرجاء إختيار الدولة")
            .NotNull().WithMessage("الرجاء إختيار الدولة");
    }
}