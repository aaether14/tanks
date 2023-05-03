using System;
using MediatR;

namespace Tanks.Application.Commands.CreateRandomMap;

public record CreateRandomMapCommand(int Width,
                                     int Height) : IRequest<Guid>;