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

    // Will return the tank that won the battle and the simulation steps.
    public (Tank, IReadOnlyList<ITankAction>) Simulate(SimulationState simulationState, Random random)
    {
        IReadOnlyList<Tank> tanks = simulationState.TankStates
            .Select(kv => kv.Value.Tank)
            .OrderBy(t => random.Next()) // The tanks start the simulation in a random order.
            .ToList();

        if (tanks.Count < 2)
        {
            throw new InvalidOperationException("At least 2 tanks are required for a simulation.");
        }
        if (tanks.DistinctBy(t => t.Id).Count() < tanks.Count)
        {
            throw new InvalidOperationException("Simulating the battle requires passing distinct tanks.");
        }

        List<ITankAction> actions = new();

        // For now, we stick to the strategy we choose at the very beginning of the simulation.
        List<(Tank, ITankAI)> aliveTanksWithAIs = tanks
            .Select(t => (t, _tankAIChooser.Choose(t, simulationState)))
            .ToList();

        // Set an upper bound in terms of simulation steps, twice the number of cells should be enough for any battle
        // that eventually finishes.
        var (mapWidth, mapHeight) = simulationState.Map.Size;
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

                ITankAction action = tankAI.ComputeNextAction(simulationState, random);
                action.Execute(simulationState, random);

                actions.Add(action);
            }
        }

        if (stepsSoFar >= maxSteps)
        {
            throw new InvalidOperationException("Simulation stopped because it was taking too long.");
        }

        // If we didn't go past maxSteps, we know we only have one tank left, which is the winner of the battle.
        var (winnerTank, _) = aliveTanksWithAIs[0];

        return (winnerTank, actions);
    }

}