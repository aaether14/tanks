using System;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Tanks.Application.Repositories;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Queries;

public class GetMapQueryHandler : IRequestHandler<GetMapQuery, GetMapQueryResult>
{

    private readonly IRepository<Map, Guid> _mapRepository;
    private readonly IMapper _mapper;

    public GetMapQueryHandler(IRepository<Map, Guid> mapRepository, IMapper mapper)
    {
        _mapRepository = mapRepository;
        _mapper = mapper;
    }

    public async Task<GetMapQueryResult> Handle(GetMapQuery request, CancellationToken cancellationToken)
    {
        Map map = await _mapRepository.GetByIdAsync(request.Id);
        
        // The map is the map where the tanks are deployed. 
        // The mapper is the object we use to an object of a type to an object of a similar type.
        // Unfortunate name conincidence. 
        return _mapper.Map<GetMapQueryResult>(map);
    }
}

