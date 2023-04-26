using System;

namespace Tanks.Application.Queries;

public record GetTankQueryResult(Guid Id,
                                 uint Health,
                                 uint AttackMin,
                                 uint AttackMax,
                                 uint DefenseMin,
                                 uint DefenseMax);