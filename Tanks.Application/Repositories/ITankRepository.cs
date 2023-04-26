using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Repositories;

public interface ITankRepository
{

    private static readonly List<Tank> _tanks = new();

    Task<Tank> GetTankByIdAsync(Guid id);

    Task AddTankAsync(Tank tank);

}