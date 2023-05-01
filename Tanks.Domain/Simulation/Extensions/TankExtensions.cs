using System;
using Tanks.Domain.DomainModels;

namespace Tanks.Domain.Simulation.Utils;

public static class TankExtensions
{
    public static bool CanShoot(this SimulationState.TankState shooterTankState, 
        SimulationState.TankState targetTankState, Map map)
    {
        var (targetTankX, targetTankY) = targetTankState.Position;
        var (shooterTankX, shooterTankY) = shooterTankState.Position;
        int shooterTankRange = shooterTankState.Tank.Range;

        if (map.Grid[shooterTankY, shooterTankX] == 1 || map.Grid[targetTankY, targetTankX] == 1)
        {
            // Either shooter tank or target tank is located in a wall position
            return false;
        }

        int dx = Math.Abs(targetTankX - shooterTankX);
        int dy = Math.Abs(targetTankY - shooterTankY);

        if (dx > shooterTankRange || dy > shooterTankRange)
        {
            // Target tank is outside the shooting range
            return false;
        }

        if (dx == 0 || dy == 0)
        {
            // Shooter tank and target tank are in the same row or column
            int sx = Math.Sign(targetTankX - shooterTankX);
            int sy = Math.Sign(targetTankY - shooterTankY);

            int x = shooterTankX;
            int y = shooterTankY;

            while (x != targetTankX || y != targetTankY)
            {
                if (map.Grid[y, x] == 1)
                {
                    // Obstacle found in the line of fire
                    return false;
                }

                x += sx;
                y += sy;
            }

            // No obstacles found in the line of fire
            return true;
        }

        return false;
    }

}