using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Tanks.Application.Queries;

public class GetTankQueryHandler : IRequestHandler<GetTankQuery, GetTankQueryResult>
{
    public async Task<GetTankQueryResult> Handle(GetTankQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new GetTankQueryResult(request.Id);
    }
}

