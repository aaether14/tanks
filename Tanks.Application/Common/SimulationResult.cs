using System;
using System.Collections.Generic;
using Tanks.Domain.DomainModels.TankActions;

namespace Tanks.Application.Common;

public record SimulationResult(Guid Id,
                               IReadOnlyList<ITankAction> Actions,
                               Guid WinnerId,
                               int Seed);