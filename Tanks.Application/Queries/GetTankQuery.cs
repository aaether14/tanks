using System;
using MediatR;

namespace Tanks.Application.Queries;

public record GetTankQuery(Guid Id) : IRequest<GetTankQueryResult>;
