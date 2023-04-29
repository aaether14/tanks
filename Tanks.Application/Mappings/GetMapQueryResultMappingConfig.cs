using Mapster;
using Tanks.Application.Queries;
using Tanks.Application.Utils;
using Tanks.Domain.DomainModels;

namespace Tanks.Application.Mappings;

public class GetMapQueryResultMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Map, GetMapQueryResult>()
            .Map(dest => dest.Grid, src => src.Grid.ToJaggedArray());
    }
}
