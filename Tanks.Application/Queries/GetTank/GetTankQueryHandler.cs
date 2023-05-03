using System;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Queries.GetTank;

public class GetTankQueryHandler : IRequestHandler<GetTankQuery, GetTankQueryResult>
{

    private readonly IRepository<Tank, Guid> _tankRepository;
    private readonly IMapper _mapper;

    public GetTankQueryHandler(IRepository<Tank, Guid>tankRepository, IMapper mapper)
    {
        _tankRepository = tankRepository;
        _mapper = mapper;
    }

    public async Task<GetTankQueryResult> Handle(GetTankQuery request, CancellationToken cancellationToken)
    {
        Tank tank = await _tankRepository.GetByIdAsync(request.Id);
        
        return _mapper.Map<GetTankQueryResult>(tank);
    }
}

