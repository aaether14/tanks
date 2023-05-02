using System;
using System.Collections.Generic;
using Tanks.Domain.DomainModels.TankActions;

namespace Tanks.Domain.DomainModels;

public class Simulation : IEntity<Guid>
{
     public Guid Id { get; set; }
     public IReadOnlyList<ITankAction> Actions { get; set; }
     public Guid WinnerId { get; set; }
     public int Seed { get; set; }

    public Simulation(IReadOnlyList<ITankAction> actions, Guid winnerId, int seed)
    {
        Id = Guid.NewGuid();
        Actions = actions;
        WinnerId = winnerId;
        Seed = seed;
    }

}