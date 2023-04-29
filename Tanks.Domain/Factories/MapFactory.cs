using System;
using System.Collections.Generic;
using Tanks.Domain.DomainModels;

namespace Tanks.Domain.Factories;

// Algorithm implemented with the help of ChatGPT. 
public class MapFactory
{

    private int[,] _grid;
    private Random _random;

    public MapFactory(int width, int height)
    {
        if (width < 1 || height < 1)
        {
            throw new ArgumentOutOfRangeException("Cannot create a map with dimensions < 1.");
        }

        _grid = new int[height, width];
        _random = new Random();
    }

    public Map CreateRandom()
    {
        InitializeLabyrinth();

        int startX = _random.Next(_grid.GetLength(1));
        int startY = _random.Next(_grid.GetLength(0));

        CarvePassages(startX, startY);

        return new Map(_grid);
    }

    private void InitializeLabyrinth()
    {
        for (int y = 0; y < _grid.GetLength(0); y++)
        {
            for (int x = 0; x < _grid.GetLength(1); x++)
            {
                _grid[y, x] = 1; // Set all cells to walls initially
            }
        }
    }

    private void CarvePassages(int x, int y)
    {
        _grid[y, x] = 0; // Set the current cell as a passage

        var directions = GetRandomDirections();

        foreach (var direction in directions)
        {
            int newX = x + direction.Item1 * 2;
            int newY = y + direction.Item2 * 2;

            if (IsInBounds(newX, newY) && _grid[newY, newX] == 1)
            {
                _grid[y + direction.Item2, x + direction.Item1] = 0; // Carve a passage in between cells

                CarvePassages(newX, newY); // Recursively carve passages from the new cell
            }
        }
    }

    private List<Tuple<int, int>> GetRandomDirections()
    {
        var directions = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(0, -1), // Up
            new Tuple<int, int>(1, 0),  // Right
            new Tuple<int, int>(0, 1),  // Down
            new Tuple<int, int>(-1, 0)  // Left
        };

        // Shuffle the directions randomly
        for (int i = 0; i < directions.Count; i++)
        {
            int randomIndex = _random.Next(directions.Count);
            var temp = directions[i];
            directions[i] = directions[randomIndex];
            directions[randomIndex] = temp;
        }

        return directions;
    }

    private bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < _grid.GetLength(1) && y >= 0 && y < _grid.GetLength(0);
    }

}