using System;
using System.Collections.Generic;
using System.Linq;
using Tanks.Domain.DomainModels;
using Tanks.Domain.DomainModels.TankActions;
using Tanks.Domain.Simulation.TanksAIs;

namespace Tanks.Domain.Simulation;

public class Simulator
{
    private readonly ITankAIChooser _tankAIChooser;

    public Simulator(ITankAIChooser tankAIChooser)
    {
        _tankAIChooser = tankAIChooser;
    }

    List<ITankAction> Simulate(List<Tank> tanks, Map map, Random random)
    {
        if (tanks.Count < 2)
        {
            throw new InvalidOperationException("At least 2 tanks are required for a simulation.");
        }

        List<ITankAction> actions = new();
        SimulationState simulationState = SimulationState.InitialState(tanks, map, random);

        // For now, we stick to the strategy we choose at the very beginning of the simulation.
        List<(Tank, ITankAI)> aliveTanksWithAIs = tanks
            .Select(t => (t, _tankAIChooser.Choose(t, simulationState)))
            .ToList();

        // Set an upper bound in terms of simulation steps, twice the number of cells should be enough for any battle
        // that eventually finishes.
        var (mapWidth, mapHeight) = map.Size;
        int maxSteps = 2 * mapWidth * mapHeight;
        int stepsSoFar = 0;

        // The simulation ends when there's a single tank left on the battlefield. 
        while (aliveTanksWithAIs.Count > 1 && stepsSoFar++ < maxSteps)
        {
            for (int i = aliveTanksWithAIs.Count - 1; i >= 0; --i)
            {
                var (tank, tankAI) = aliveTanksWithAIs[i];

                // Remove dead tanks.
                if (!tank.Alive)
                {
                    aliveTanksWithAIs.RemoveAt(i);
                    continue;
                }

                actions.Add(tankAI.ComputeNextAction(simulationState, random));
            }
        }

        if (stepsSoFar >= maxSteps)
        {
            throw new InvalidOperationException("Simulation stops because it was taking too long.");
        }

        return actions;
    }

}