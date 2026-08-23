using FluentValidation;
using TrafficFineSystem.Dtos.TrafficFineDtos;

namespace TrafficFineSystem.Validators.TrafficFineValidators
{
    public class CreateTrafficFineValidator : AbstractValidator<CreateTrafficFineDto>
    {
        public CreateTrafficFineValidator()
        {
            RuleFor(x => x.VehicleId).GreaterThan(0).WithMessage("Araç seçimi zorunludur.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Ceza tutarı 0'dan büyük olmalıdır.");

            RuleFor(x => x.FineDate).NotEmpty().WithMessage("Ceza tarihi zorunludur.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı zorunludur.").MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}
