using Backend.Data.DTOs.Country;
using FluentValidation;

namespace Backend.Validations;

public class CountryValidation : AbstractValidator<UpsertCountry>
{
    public CountryValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("الإسم مطلوب")
            .NotNull().WithMessage("الإسم مطلوب")
            .MaximumLength(50).WithMessage("عدد حروف الإسم لايجب أن تتعدى ال 50 حرف");
        
        RuleFor(c => c.CountryCode)
            .NotEmpty().WithMessage("رمز الدولة مطلوب")
            .NotNull().WithMessage("رمز الدولة مطلوب")
            .MaximumLength(3).WithMessage("عدد حروف رمز الدولة لايجب أن تتعدى ال 3 حرف");
    }
}