using System;
using System.Collections.Generic;
using System.Linq;
using Tanks.Domain.DomainModels;
using Tanks.Domain.Simulation.Utils;

namespace Tanks.Domain.Simulation;

public class SimulationState
{

    public Dictionary<Guid, TankState> TankStates { get; }
    public Map Map { get; }

    private SimulationState(Dictionary<Guid, TankState> tankStates, Map map)
    {
        TankStates = tankStates;
        Map = map;
    }

    public static SimulationState InitialState(List<Tank> tanks, Map map, Random random)
    {
        Dictionary<Guid, TankState> tankStates = 
            tanks.Zip<Tank, (int, int)>(map.RandomEmptyPositions(random))
            .ToDictionary(p => p.First.Id, p => new TankState(p.First, p.Second));

        // If there are not enough random positions to spawn the give tanks at, issue an error. 
        if (tankStates.Count < tanks.Count)
        {
            throw new ArgumentException("Not enough places to spawn the tanks on the given map.");
        }

        return new SimulationState(tankStates, map);
    }

    public class TankState
    {

        public Tank Tank { get; }
        public (int, int) Position { get; set; }

        public TankState(Tank tank, (int, int) initialPosition)
        {
            Tank = tank;
            Position = initialPosition;
        }

    }
}