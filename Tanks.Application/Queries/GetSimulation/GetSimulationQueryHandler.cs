using System;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Tanks.Application.Common;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Queries.GetSimulation;

public class GetSimulationQueryHandler : IRequestHandler<GetSimulationQuery, SimulationResult>
{
    private readonly IRepository<Simulation, Guid> _simulationRepository;
    private readonly IMapper _mapper;

    public GetSimulationQueryHandler(IRepository<Simulation, Guid> simulationRepository, IMapper mapper)
    {
        _simulationRepository = simulationRepository;
        _mapper = mapper;
    }

    public async Task<SimulationResult> Handle(GetSimulationQuery request, CancellationToken cancellationToken)
    {
        Simulation simulation = await _simulationRepository.GetByIdAsync(request.Id);

        return _mapper.Map<SimulationResult>(simulation);
    }
}
