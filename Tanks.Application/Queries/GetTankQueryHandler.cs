using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Queries;

public class GetTankQueryHandler : IRequestHandler<GetTankQuery, GetTankQueryResult>
{

    private readonly ITankRepository _tankRepository;

    public GetTankQueryHandler(ITankRepository tankRepository)
    {
        _tankRepository = tankRepository;
    }

    public async Task<GetTankQueryResult> Handle(GetTankQuery request, CancellationToken cancellationToken)
    {
        Tank tank = await _tankRepository.GetTankByIdAsync(request.Id);
        
        return new GetTankQueryResult(
            Id: tank.Id, 
            Health: tank.Health,
            AttackMin: tank.AttackMin,
            AttackMax: tank.AttackMax,
            DefenseMin: tank.DefenseMin,
            DefenseMax: tank.DefenseMax);
    }
}

