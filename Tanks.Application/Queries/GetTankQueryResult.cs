using System;

namespace Tanks.Application.Queries;

public record GetTankQueryResult(Guid Id,
                                 int Health,
                                 int AttackMin,
                                 int AttackMax,
                                 int DefenseMin,
                                 int DefenseMax, 
                                 int Range);