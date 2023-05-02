using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tanks.Application.Common;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;
using Tanks.Domain.Simulation;
using MapsterMapper;
using Force.DeepCloner;

namespace Tanks.Application.Commands;

public class SimulateCommandHander : IRequestHandler<SimulateCommand, SimulationResult>
{

    private readonly IRepository<Tank, Guid> _tankRepository;
    private readonly IRepository<Map, Guid> _mapRepository;
    private readonly IRepository<Simulation, Guid> _simulationRepository;
    private readonly Simulator _simulator;
    private readonly IMapper _mapper;

    public SimulateCommandHander(Simulator simulator,
                                 IRepository<Tank, Guid> tankRepository,
                                 IRepository<Map, Guid> mapRepository,
                                 IRepository<Simulation, Guid> simulationRepository,
                                 IMapper mapper)
    {
        _simulator = simulator;
        _tankRepository = tankRepository;
        _mapRepository = mapRepository;
        _simulationRepository = simulationRepository;
        _mapper = mapper;
    }

    public async Task<SimulationResult> Handle(SimulateCommand request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Tank> tanks = await Task.WhenAll(request.TankIds
            .Select(id => _tankRepository.GetByIdAsync(id))
            .ToArray());

        Map map = await _mapRepository.GetByIdAsync(request.MapId);

        int seed = request.Seed ?? Random.Shared.Next();
        Random random = new Random(seed);

        SimulationState initialSimulationState = SimulationState.InitialState(tanks, map, random);
        SimulationState initialSimulationStateCloned = initialSimulationState.DeepClone();

        var (winnerTank, actions) = _simulator.Simulate(initialSimulationState, random);

        Simulation simulation = new Simulation(winnerTank.Id,
                                               seed,
                                               initialSimulationStateCloned,
                                               actions);

        await _simulationRepository.AddAsync(simulation);

        return _mapper.Map<SimulationResult>(simulation);
    }
    
}
