using FluentValidation;
using TrafficFineSystem.Dtos.AccountDtos;

namespace TrafficFineSystem.Validators.AccountValidators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı zorunludur.").EmailAddress() .WithMessage("Geçerli bir email adresi giriniz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı zorunludur.");
        }
    }
}
