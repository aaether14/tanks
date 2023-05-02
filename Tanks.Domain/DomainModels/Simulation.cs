using System;
using System.Collections.Generic;
using Tanks.Domain.DomainModels.TankActions;
using Tanks.Domain.Simulation;

namespace Tanks.Domain.DomainModels;

public class Simulation : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid WinnerId { get; set; }
    public int Seed { get; set; }
    public SimulationState InitialState { get; set; }
    public IReadOnlyList<ITankAction> Actions { get; set; }

    public Simulation(Guid winnerId, int seed, SimulationState initialState, IReadOnlyList<ITankAction> actions)
    {
        Id = Guid.NewGuid();
        WinnerId = winnerId;
        Seed = seed;
        InitialState = initialState;
        Actions = actions;
    }

}