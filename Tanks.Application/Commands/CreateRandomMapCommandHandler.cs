using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;
using Tanks.Domain.Factories;

namespace Tanks.Application.Commands;

public class CreateRandomMapCommandHandler : IRequestHandler<CreateRandomMapCommand, Guid>
{

    private readonly IRepository<Map, Guid> _mapRepository;
    private readonly IMapFactory _mapFactory;

    public CreateRandomMapCommandHandler(IRepository<Map, Guid> mapRepository, IMapFactory mapFactory)
    {
        _mapRepository = mapRepository;
        _mapFactory = mapFactory;
    }

    public async Task<Guid> Handle(CreateRandomMapCommand request, CancellationToken cancellationToken)
    {
        // First,  create the new map;
        Map map = _mapFactory.Create(request.width, request.height);

        // Then, add it to the repository.
        await _mapRepository.AddAsync(map);

        // Last, return the new map's id.
        return map.Id;
    }

}
