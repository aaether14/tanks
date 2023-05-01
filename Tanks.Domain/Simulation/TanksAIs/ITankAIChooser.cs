using Tanks.Domain.DomainModels;

namespace Tanks.Domain.Simulation.TanksAIs;

public interface ITankAIChooser
{
    public ITankAI Choose(Tank tank, SimulationState simulationState);
}