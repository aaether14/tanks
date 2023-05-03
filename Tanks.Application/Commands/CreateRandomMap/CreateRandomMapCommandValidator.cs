using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Tanks.Application.Commands.CreateRandomMap;

public class CreateRandomMapCommandValidator : AbstractValidator<CreateRandomMapCommand>
{
    public CreateRandomMapCommandValidator(IConfiguration configuration)
    {
        CreateRandomMapConfig createRandomMapConfig = configuration
            .GetSection(CreateRandomMapConfig.SectionName)
            .Get<CreateRandomMapConfig>()!;
        
        RuleFor(command => command.Width)
            .GreaterThanOrEqualTo(createRandomMapConfig.MinWidth)
            .WithMessage($"Width must be greater than or equal to {createRandomMapConfig.MinWidth}.");
        RuleFor(command => command.Height)
            .GreaterThanOrEqualTo(createRandomMapConfig.MinHeight)
            .WithMessage($"Height must be greater than or equal to {createRandomMapConfig.MinHeight}.");
    }

}
