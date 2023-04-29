using System;
using MediatR;

namespace Tanks.Application.Queries;

public record GetMapQueryResult(Guid Id,
                                int[][] Grid);