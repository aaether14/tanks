using System;
using System.Threading.Tasks;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Repositories;

public interface IMapRepository
{

    Task<Map> GetMapByIdAsync(Guid id);

    Task AddMapAsync(Map tank);

}