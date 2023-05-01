using System.Collections.Generic;

namespace Tanks.Domain.Simulation.PathFinding;

public interface IPathFinder
{
    List<(int, int)>? FindPath(int[,] grid, (int, int) start, (int, int) end);
}