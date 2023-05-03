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

namespace Tanks.Application.Commands.Simulate;

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
        // Get all the required resources from the respective repositories.
        IReadOnlyList<Tank> tanks = await Task.WhenAll(request.TankIds
            .Select(id => _tankRepository.GetByIdAsync(id))
            .ToArray());

        Map map = await _mapRepository.GetByIdAsync(request.MapId);

        // We want the simulation to be completely deterministic.
        int seed = request.Seed ?? Random.Shared.Next();
        Random random = new Random(seed);

        // We deep copy the initial state because we want to include it in the simulation object.
        SimulationState initialSimulationState = SimulationState.InitialState(tanks, map, random);
        SimulationState initialSimulationStateCloned = initialSimulationState.DeepClone();

        // Run the simulation on the initial state.
        var (winnerTank, actions) = _simulator.Simulate(initialSimulationState, random);

        // Create the simulation object then add it to the repository.
        Simulation simulation = new Simulation(winnerTank.Id,
                                               seed,
                                               initialSimulationStateCloned,
                                               actions);

        await _simulationRepository.AddAsync(simulation);

        // Map to the expected result and return.
        return _mapper.Map<SimulationResult>(simulation);
    }
    
}
