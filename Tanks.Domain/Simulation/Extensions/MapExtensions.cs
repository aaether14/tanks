using System;
using System.Collections.Generic;
using System.Linq;
using Tanks.Domain.DomainModels;

namespace Tanks.Domain.Simulation.Utils;

public static class MapExtensions
{

    public static IEnumerable<(int, int)> RandomEmptyPositions(this Map map, Random random)
    {
        List<(int, int)> availablePositions = new List<(int, int)>();

        // Find available positions with 0
        for (int y = 0; y < map.Grid.GetLength(0); y++)
        {
            for (int x = 0; x < map.Grid.GetLength(1); x++)
            {
                if (map.Grid[y, x] == 0)
                {
                    availablePositions.Add((x, y));
                }
            }
        }

        // Shuffle the available positions
        return availablePositions.OrderBy(x => random.Next());
    }

}