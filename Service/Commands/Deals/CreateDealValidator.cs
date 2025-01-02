using Data.Models;
using FluentValidation;

namespace Service.Commands.Deals;

public class CreateDealValidator : AbstractValidator<CreateDealCommand>
{
    public CreateDealValidator()
    {
        RuleFor(deal => deal.Deal.Name)
            .NotEmpty();

        RuleFor(deal => deal.Deal.Description)
            .NotEmpty();
    }
}