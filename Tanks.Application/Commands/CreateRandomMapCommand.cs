using System;
using MediatR;

namespace Tanks.Application.Commands;

public record CreateRandomMapCommand(int width,
                                     int height) : IRequest<Guid>;