using Backend.Data.DTOs.Journey;
using FluentValidation;

namespace Backend.Validations;

public class JourneyValidation : AbstractValidator<UpsertJourney>
{
    public JourneyValidation()
    {
        RuleFor(j => j.DepDate)
            .NotEmpty().WithMessage("تاريخ القيام مطلوب")
            .NotNull().WithMessage("تاريخ القيام مطلوب");

        RuleFor(j => j.ArrDate)
            .NotEmpty().WithMessage("تاريخ الوصول مطلوب")
            .NotNull().WithMessage("تاريخ الوصول مطلوب");

        RuleFor(j => j.DepCityId)
            .NotEmpty().WithMessage("الرجاء إختيار جهة القيام")
            .NotNull().WithMessage("الرجاء إختيار جهة القيام");

        RuleFor(j => j.ArrCityId)
        .NotEmpty().WithMessage("الرجاء إختيار جهة الوصول")
        .NotNull().WithMessage("الرجاء إختيار جهة الوصول");

        RuleFor(j => j.DriverId)
            .NotEmpty().WithMessage("الرجاء إختيار السائق")
            .NotNull().WithMessage("الرجاء إختيار السائق");

        RuleFor(j => j.VehicleId)
            .NotEmpty().WithMessage("الرجاء إختيار المركبة")
            .NotNull().WithMessage("الرجاء إختيار المركبة");

        RuleFor(j => j.CompanyId)
            .NotEmpty().WithMessage("الرجاء إختيار الشركة")
            .NotNull().WithMessage("الرجاء إختيار الشركة");

        RuleFor(j => j.JourneyType)
            .NotNull().WithName("نوع الرحلة مطلوب")
            .NotEmpty().WithName("نوع الرحلة مطلوب")
            .IsEnumName(typeof(JourneyType)).WithMessage("نوع الرحلة يجب أن يكون شحن أو ركاب");
    }

    private enum JourneyType
    {
        شحن,
        ركاب
    }
}
