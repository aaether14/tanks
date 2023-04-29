using System;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Queries;

public class GetTankQueryHandler : IRequestHandler<GetTankQuery, GetTankQueryResult>
{

    private readonly ITankRepository _tankRepository;
    private readonly IMapper _mapper;

    public GetTankQueryHandler(ITankRepository tankRepository, IMapper mapper)
    {
        _tankRepository = tankRepository;
        _mapper = mapper;
    }

    public async Task<GetTankQueryResult> Handle(GetTankQuery request, CancellationToken cancellationToken)
    {
        Tank? tank = await _tankRepository.GetTankByIdAsync(request.Id);

        if (tank is null)
        {
            throw new ArgumentException($"Cannot find any tank with the id {request.Id}.");
        }
        
        return _mapper.Map<GetTankQueryResult>(tank);
    }
}

