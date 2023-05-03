using System;
using MediatR;

namespace Tanks.Application.Queries.GetMap;

public record GetMapQueryResult(Guid Id,
                                int[][] Grid);