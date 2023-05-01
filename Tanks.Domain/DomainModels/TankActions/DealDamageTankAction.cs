using System;
using System.Collections.Generic;
using Tanks.Domain.Simulation;

namespace Tanks.Domain.DomainModels.TankActions;

public record DealDamageTankAction(Guid SourceTankId,
                                   Guid TargetTankId,
                                   int DamageAmount) : ITankAction
{

    public void Execute(SimulationState simulationState, Random random)
    {
        simulationState.TankStates[TargetTankId].Tank.TakeDamage(DamageAmount, random);
    }
}