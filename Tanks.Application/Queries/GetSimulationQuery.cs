using System;
using MediatR;
using Tanks.Application.Common;

namespace Tanks.Application.Queries;

public record GetSimulationQuery(Guid Id) : IRequest<SimulationResult>;