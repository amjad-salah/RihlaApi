using Backend.Data.DTOs.Driver;
using FluentValidation;

namespace Backend.Validations;

public class DriverValidation : AbstractValidator<UpsertDriver>
{
    public DriverValidation()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("الإسم مطلوب")
            .NotNull().WithMessage("الإسم مطلوب")
            .MaximumLength(200).WithMessage("عدد حروف الإسم لايجب أن تتعدى ال 200 حرف");
        
        RuleFor(d => d.CompanyId)
            .NotEmpty().WithMessage("الرجاء إختيار الشركة")
            .NotNull().WithMessage("الرجاء إختيار الشركة");
        
        RuleFor(d => d.LicenseNo)
            .NotEmpty().WithMessage("رقم الرخصة مطلوب")
            .NotNull().WithMessage("رقم الرخصة مطلوب")
            .MaximumLength(200).WithMessage("عدد حروف العنوان لايجب أن تتعدى ال 200 حرف");
        
        RuleFor(c => c.PhoneNo)
            .NotEmpty().WithMessage("رقم الهاتف مطلوب")
            .NotNull().WithMessage("رقم الهاتف مطلوب")
            .MaximumLength(15).WithMessage("عدد حروف رقم الهاتف لايجب أن تتعدى ال 15 حرف");
    }
}