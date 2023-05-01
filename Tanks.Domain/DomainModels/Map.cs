using System;

namespace Tanks.Domain.DomainModels;

public class Map
{

    public Guid Id { get; set; }
    public int[,] Grid { get; set; }

    // (width, height)
    public (int, int) Size => (Grid.GetLength(1), Grid.GetLength(0));

    public Map(int[,] grid)
    {
        Id = Guid.NewGuid();
        Grid = grid;
    }

}

