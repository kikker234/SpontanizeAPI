using Data;
using FluentValidation;

namespace Service.Commands.Organizations;

public class CreateOrganizationValidator : AbstractValidator<CreateOrganizationRequest>
{
    private SpontanizeContext _context;
    
    public CreateOrganizationValidator(SpontanizeContext context)
    {
        _context = context;

        RuleFor(req => req.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty!");

        RuleFor(req => req.Name)
            .Must(UniqueName)
            .WithMessage("Name must be unique");
    }
    
    private bool UniqueName(CreateOrganizationRequest request, string name)
    {
        return !_context.Organization
            .Any(x => x.Name.ToLower() == name.ToLower());
    }

}