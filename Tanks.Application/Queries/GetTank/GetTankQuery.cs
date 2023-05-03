using System;
using MediatR;

namespace Tanks.Application.Queries.GetTank;

public record GetTankQuery(Guid Id) : IRequest<GetTankQueryResult>;
