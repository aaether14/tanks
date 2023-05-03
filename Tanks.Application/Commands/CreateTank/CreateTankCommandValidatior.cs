using FluentValidation;

namespace Tanks.Application.Commands.CreateTank;

public class CreateTankCommandValidator : AbstractValidator<CreateTankCommand>
{
    public CreateTankCommandValidator()
    {
        RuleFor(command => command.Health)
            .GreaterThan(0)
            .WithMessage("Health must be greater than 0.");
        RuleFor(command => command.AttackMin)
            .GreaterThanOrEqualTo(0)
            .WithMessage("AttackMin must be greater than or equal to 0.");
        RuleFor(command => command.AttackMax)
            .GreaterThanOrEqualTo(command => command.AttackMin)
            .WithMessage("AttackMax must be greater than or equal to AttackMin.");
        RuleFor(command => command.DefenseMin)
            .GreaterThanOrEqualTo(0)
            .WithMessage("DefenseMin must be greater than or equal to 0.");
        RuleFor(command => command.DefenseMax)
            .GreaterThanOrEqualTo(command => command.DefenseMin)
            .WithMessage("DefenseMax must be greater than or equal to DefenseMin.");
        RuleFor(command => command.Range)
            .GreaterThan(0)
            .WithMessage("Range must be greater than 0.");
    }
}
