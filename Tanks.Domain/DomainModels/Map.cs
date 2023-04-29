using System;
using Tanks.Domain.Factories;

namespace Tanks.Domain.DomainModels;

public class Map
{

    public Guid Id { get; }
    public int[,] Grid { get; }

    private Map(int[,] grid)
    {
        Id = Guid.NewGuid();
        Grid = grid;
    }

    public static MapFactory GetFactory(int width, int height)
    {
        return new MapFactory(grid => new Map(grid), width, height);
    }

}

