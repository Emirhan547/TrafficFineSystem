using FluentValidation;
using TrafficFineSystem.Dtos.ApprovalHistoryDtos;

namespace TrafficFineSystem.Validators.ApproveValidators
{
    public class RejectTrafficFineValidator: AbstractValidator<RejectTrafficFineDto>
    {
        public RejectTrafficFineValidator()
        {
            RuleFor(x => x.TrafficFineId).GreaterThan(0).WithMessage("Geçersiz trafik cezası.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Ret nedeni zorunludur.").MaximumLength(500).WithMessage("Ret nedeni en fazla 500 karakter olabilir.");
        }
    }
}
