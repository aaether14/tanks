using System;
using System.Linq;
using Mapster;
using Tanks.Application.Commands;
using Tanks.Application.Common;
using Tanks.Application.Queries;
using Tanks.Application.Utils;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Mappings;

public class MappingConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateTankCommand, Tank>()
            .MapToConstructor(true);

        config.ForType<Map, GetMapQueryResult>()
            .Map(dest => dest.Grid, src => src.Grid.ToJaggedArray());

        config.ForType<Simulation, SimulationResult>()
            .Map(dest => dest.Map, src => src.InitialState.Map)
            .Map(dest => dest.InitialTankStates, src 
                => src.InitialState.TankStates.Values);
    }
}