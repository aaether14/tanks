using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Infrastructure.Repositories;

public class TankRepository : ITankRepository
{

    private static readonly List<Tank> _tanks = new();

    async Task<Tank> ITankRepository.GetTankByIdAsync(Guid id)
    {
        await Task.CompletedTask;
        return _tanks.Find(tank => tank.Id.Equals(id))!;
    }

    async Task ITankRepository.AddTankAsync(Tank tank)
    {
        await Task.CompletedTask;
        _tanks.Add(tank);
    }
}