using System;
using MediatR;

namespace Tanks.Application.Queries;

public record GetMapQuery(Guid Id) : IRequest<GetMapQueryResult>;