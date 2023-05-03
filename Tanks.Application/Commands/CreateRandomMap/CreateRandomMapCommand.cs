using System;
using MediatR;

namespace Tanks.Application.Commands.CreateRandomMap;

public record CreateRandomMapCommand(int width,
                                     int height) : IRequest<Guid>;