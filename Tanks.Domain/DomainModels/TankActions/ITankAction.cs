using System;
using Tanks.Domain.Simulation;

namespace Tanks.Domain.DomainModels.TankActions;

public interface ITankAction
{
    void Execute(SimulationState simulationState, Random random);
}