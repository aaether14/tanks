using System;
using System.Linq;
using Tanks.Domain.DomainModels;
using Tanks.Domain.Simulation.PathFinding;

namespace Tanks.Domain.Simulation.TanksAIs;

public class InitialTankAIChooser : ITankAIChooser
{
    private readonly IPathFinder _pathFinder;

    public InitialTankAIChooser(IPathFinder pathFinder)
    {
        _pathFinder = pathFinder;
    }

    public ITankAI Choose(Tank tank, SimulationState simulationState)
    {
        Tank enemyTank = simulationState.TankStates.Values
            .FirstOrDefault(s => !s.Tank.Id.Equals(tank.Id))
            ?.Tank 
            ?? throw new InvalidOperationException("Cannot find an enemy tank.");

        return new TankAI(tank.Id, enemyTank.Id, _pathFinder);
    }
}