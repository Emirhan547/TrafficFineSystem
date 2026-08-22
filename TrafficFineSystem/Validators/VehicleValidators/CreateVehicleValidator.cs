using FluentValidation;
using TrafficFineSystem.Dtos.VehicleDtos;

namespace TrafficFineSystem.Validators.VehicleValidators
{
    public class CreateVehicleValidator : AbstractValidator<CreateVehicleDto>
    {
        public CreateVehicleValidator()
        {
            RuleFor(x => x.Plate)
               .NotEmpty()
               .WithMessage("Plaka alanı zorunludur.")
               .MaximumLength(20)
               .WithMessage("Plaka en fazla 20 karakter olabilir.");

            RuleFor(x => x.VehicleType)
                .IsInEnum()
                .WithMessage("Geçerli bir araç tipi seçilmelidir.");

            RuleFor(x => x.Brand)
                .NotEmpty()
                .WithMessage("Marka alanı zorunludur.")
                .MaximumLength(100)
                .WithMessage("Marka en fazla 100 karakter olabilir.");

            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Model alanı zorunludur.")
                .MaximumLength(100)
                .WithMessage("Model en fazla 100 karakter olabilir.");
        }
    }
}
