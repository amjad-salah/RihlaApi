using Backend.Data.DTOs.Vehicle;
using FluentValidation;

namespace Backend.Validations;

public class VehicleValidation : AbstractValidator<UpsertVehicle>
{
    public VehicleValidation()
    {
        RuleFor(v => v.Model)
            .NotNull().WithName("موديل المركبة مطلوب")
            .NotEmpty().WithName("موديل المركبة مطلوب")
            .MaximumLength(100).WithMessage("عدد حروف الموديل لايجب أن تتعدى ال 100 حرف");
        
        RuleFor(v => v.PlateNo)
            .NotNull().WithName("رقم اللوحة مطلوب")
            .NotEmpty().WithName("رقم اللوحة مطلوب")
            .MaximumLength(30).WithMessage("عدد حروف رقم اللوحة لايجب أن تتعدى ال 30 حرف");
        
        RuleFor(d => d.CompanyId)
            .NotEmpty().WithMessage("الرجاء إختيار الشركة")
            .NotNull().WithMessage("الرجاء إختيار الشركة");
        
        RuleFor(v => v.VehicleType)
            .NotNull().WithName("نوع المركبة مطلوب")
            .NotEmpty().WithName("نوع المركبة مطلوب")
            .IsEnumName(typeof(VehicleType)).WithMessage("نوع المركبة يجب أن يكون شاحنة أو ركاب");
    }

    private enum VehicleType
    {
        شاحنة,
        ركاب
    }
}