using System;
using MediatR;

namespace Tanks.Application.Queries.GetMap;

public record GetMapQuery(Guid Id) : IRequest<GetMapQueryResult>;