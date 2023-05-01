using System;
using Tanks.Domain.DomainModels.TankActions;

namespace Tanks.Domain.Simulation.TanksAIs;

public interface ITankAI
{
    ITankAction ComputeNextAction(SimulationState simulationState, Random random);
}