using System;
using MediatR;

namespace Tanks.Application.Commands;

public record CreateTankCommand(uint Health,
                                uint AttackMin,
                                uint AttackMax,
                                uint DefenseMin,
                                uint DefenseMax) : IRequest<Guid>;