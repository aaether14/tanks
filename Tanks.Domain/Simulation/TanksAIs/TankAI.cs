using System;
using System.Collections.Generic;
using Tanks.Domain.DomainModels.TankActions;
using Tanks.Domain.Simulation.PathFinding;
using Tanks.Domain.Simulation.Utils;

namespace Tanks.Domain.Simulation.TanksAIs;

public class TankAI : ITankAI
{

    private readonly Guid _controlledTankId;
    private readonly Guid _enemyTankId;
    private readonly IPathFinder _pathFinder;

    public TankAI(Guid controlledTankId, Guid enemyTankId, IPathFinder pathFinder)
    {
        _controlledTankId = controlledTankId;
        _enemyTankId = enemyTankId;
        _pathFinder = pathFinder;
    }

    public ITankAction ComputeNextAction(SimulationState simulationState, Random random)
    {
        SimulationState.TankState controlledTankState = simulationState.TankStates[_controlledTankId];
        SimulationState.TankState enemyTankState = simulationState.TankStates[_enemyTankId];

        // Either we can shoot at the enemy tank, or we start moving towards it. 
        if (controlledTankState.CanShoot(enemyTankState, simulationState.Map))
        {
            return new DealDamageTankAction(controlledTankState.Tank.Id, enemyTankState.Tank.Id, 
                controlledTankState.Tank.RollDamage(random));
        }
        else 
        {
            // We assume there's always a path, because we generate maps such that they describe connected graphs.
            List<(int, int)> path = _pathFinder.FindPath(simulationState.Map.Grid, controlledTankState.Position, 
                enemyTankState.Position)!;
            return new MoveTankAction(controlledTankState.Tank.Id, path[0]);
        }
    }

}