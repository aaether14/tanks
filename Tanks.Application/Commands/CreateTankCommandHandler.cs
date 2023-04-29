using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Commands;

public class CreateTankCommandHandler : IRequestHandler<CreateTankCommand, Guid>
{

    private readonly ITankRepository _tankRepository;

    public CreateTankCommandHandler(ITankRepository tankRepository)
    {
        _tankRepository = tankRepository;
    }

    public async Task<Guid> Handle(CreateTankCommand request, CancellationToken cancellationToken)
    {
        // First, create the tank.
        Tank tank = new Tank(health: request.Health,
                             attackMin: request.AttackMin,
                             attackMax: request.AttackMax,
                             defenseMin: request.DefenseMin,
                             defenseMax: request.DefenseMax);

        // Then, add to repository.
        await _tankRepository.AddTankAsync(tank);

        // Last, return the the new tank's id.
        return tank.Id;
    }
}
