using FluentValidation;
using TrafficFineSystem.Dtos.ApprovalHistoryDtos;

namespace TrafficFineSystem.Validators.ApproveValidators
{
    public class ApproveTrafficFineValidator: AbstractValidator<ApproveTrafficFineDto>
    {
        public ApproveTrafficFineValidator()
        {
            RuleFor(x => x.TrafficFineId).GreaterThan(0).WithMessage("Geçersiz trafik cezası.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}
