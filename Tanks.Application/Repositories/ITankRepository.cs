using System;
using System.Threading.Tasks;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Repositories;

public interface ITankRepository
{

    Task<Tank?> GetTankByIdAsync(Guid id);

    Task AddTankAsync(Tank tank);

}