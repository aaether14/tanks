using System;
using System.Collections.Generic;
using MediatR;
using Tanks.Application.Common;

namespace Tanks.Application.Commands.Simulate;

public record SimulateCommand(IReadOnlyList<Guid> TankIds,
                              Guid MapId,
                              int? Seed) : IRequest<SimulationResult>; 