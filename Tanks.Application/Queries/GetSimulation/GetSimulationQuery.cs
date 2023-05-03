using System;
using MediatR;
using Tanks.Application.Common;

namespace Tanks.Application.Queries.GetSimulation;

public record GetSimulationQuery(Guid Id) : IRequest<SimulationResult>;