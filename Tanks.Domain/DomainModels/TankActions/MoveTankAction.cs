using System;
using Tanks.Domain.Simulation;

namespace Tanks.Domain.DomainModels.TankActions;

public record MoveTankAction(Guid TankId,
                             (int, int) NewPosition) : ITankAction
{

    public void Execute(SimulationState simulationState, Random _)
    {
        simulationState.TankStates[TankId].Position = NewPosition;
    }
}