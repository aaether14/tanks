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

    private readonly IMapRepository _mapRepository;

    public CreateRandomMapCommandHandler(IMapRepository mapRepository)
    {
        _mapRepository = mapRepository;
    }

    public async Task<Guid> Handle(CreateRandomMapCommand request, CancellationToken cancellationToken)
    {
        // First, create generate the new map.
        Map map = new MapFactory(request.width, request.height).CreateRandom();

        // Then, add it to the repository.
        await _mapRepository.AddMapAsync(map);

        // Last, return the new map's id.
        return map.Id;
    }

}
