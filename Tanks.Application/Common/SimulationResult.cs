using System;
using System.Collections.Generic;
using Tanks.Application.Queries.GetMap;
using Tanks.Application.Queries.GetTank;
using Tanks.Domain.DomainModels.TankActions;

namespace Tanks.Application.Common;

public record SimulationTankState (GetTankQueryResult Tank,
                                   (int, int) Position);

public record SimulationResult(Guid Id,
                               Guid WinnerId,
                               int Seed,
                               IReadOnlyList<SimulationTankState> InitialTankStates,
                               IReadOnlyList<ITankAction> Actions,
                               GetMapQueryResult Map);