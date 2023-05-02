using System;
using System.Collections.Generic;

namespace Tanks.Domain.Simulation.PathFinding;

public class AStarPathFinder : IPathFinder
{
    public IReadOnlyList<(int, int)>? FindPath(int[,] grid, (int, int) start, (int, int) end)
    {
        int width = grid.GetLength(1);
        int height = grid.GetLength(0);

        // Check if the start and end positions are the same or blocked by obstacles
        if (start.Equals(end) || grid[start.Item2, start.Item1] == 1 || grid[end.Item2, end.Item1] == 1)
            return null;

        // Create a priority queue to store nodes to be explored
        var frontier = new PriorityQueue<(int, int), int>();
        frontier.Enqueue(start, 0);

        // Initialize dictionaries to store the path and cost for each node
        var cameFrom = new Dictionary<(int, int), (int, int)>();
        var costSoFar = new Dictionary<(int, int), int>();

        cameFrom[start] = default;
        costSoFar[start] = 0;

        // Main loop that explores nodes until the goal is reached or there are no more nodes to explore
        while (frontier.Count > 0)
        {
            // Dequeue the node with the lowest priority (based on the cost) from the frontier
            var current = frontier.Dequeue();

            // Check if the goal has been reached
            if (current.Equals(end))
            {
                break;
            }

            // Iterate over the neighbors of the current node
            foreach (var neighbor in GetNeighbors(grid, current, width, height))
            {
                // Calculate the new cost to reach the neighbor node
                int newCost = costSoFar[current] + 1;

                // Check if the neighbor has not been visited or the new path to the neighbor is better
                if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                {
                    // Update the cost and priority of the neighbor
                    costSoFar[neighbor] = newCost;
                    int priority = newCost + ManhattanDistance(neighbor, end);

                    // Enqueue the neighbor with the updated priority
                    frontier.Enqueue(neighbor, priority);

                    // Update the path from the start to the neighbor
                    cameFrom[neighbor] = current;
                }
            }
        }

        // Reconstruct the path from the cameFrom dictionary
        return ReconstructPath(cameFrom, start, end);
    }

    private static List<(int, int)> GetNeighbors(int[,] grid, (int, int) node, int width, int height)
    {
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        List<(int, int)> neighbors = new List<(int, int)>();

        // Iterate over the possible neighbor positions
        for (int i = 0; i < 4; i++)
        {
            int nx = node.Item1 + dx[i];
            int ny = node.Item2 + dy[i];

            // Check if the neighbor position is within the grid bounds and is not blocked
            if (nx >= 0 && nx < width && ny >= 0 && ny < height && grid[ny, nx] == 0)
            {
                neighbors.Add((nx, ny));
            }
        }

        return neighbors;
    }

    private List<(int, int)> ReconstructPath(Dictionary<(int, int), (int, int)> cameFrom, (int, int) start, (int, int) end)
    {
        List<(int, int)> path = new List<(int, int)>();

        var current = end;

        // Starting from the end position, follow the path backwards by retrieving the previous node from the cameFrom dictionary
        while (!current.Equals(start))
        {
            path.Add(current);
            current = cameFrom[current];
        }

        // Reverse the path to get it from start to end
        path.Reverse();
        return path;
    }

    private int ManhattanDistance((int, int) node1, (int, int) node2)
    {
        // Calculate the Manhattan distance between two nodes (the sum of the absolute differences in x and y coordinates)
        return Math.Abs(node1.Item1 - node2.Item1) + Math.Abs(node1.Item2 - node2.Item2);
    }

}