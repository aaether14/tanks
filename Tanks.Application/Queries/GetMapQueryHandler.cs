using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Queries;

public class GetMapQueryHandler : IRequestHandler<GetMapQuery, GetMapQueryResult>
{

    private readonly IMapRepository _mapRepository;

    public GetMapQueryHandler(IMapRepository mapRepository)
    {
        _mapRepository = mapRepository;
    }

    public async Task<GetMapQueryResult> Handle(GetMapQuery request, CancellationToken cancellationToken)
    {
        Map tank = await _mapRepository.GetMapByIdAsync(request.Id);
        
        return new GetMapQueryResult(
            Id: tank.Id,
            Grid: tank.Grid
        );
    }
}

