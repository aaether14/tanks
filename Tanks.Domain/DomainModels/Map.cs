using System;

namespace Tanks.Domain.DomainModels;

public class Map
{

    public Guid Id { get; set; }
    public int[,] Grid { get; set; }

    public Map(int[,] grid)
    {
        Id = Guid.NewGuid();
        Grid = grid;
    }

}

