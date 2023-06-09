using System;
using MediatR;

namespace Tanks.Application.Commands.CreateTank;

public record CreateTankCommand(int Health,
                                int AttackMin,
                                int AttackMax,
                                int DefenseMin,
                                int DefenseMax, 
                                int Range) : IRequest<Guid>;