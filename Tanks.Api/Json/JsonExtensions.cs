// Inspiration drawn from https://stackoverflow.com/a/75933576

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Tanks.Application.Queries;
using Tanks.Domain.DomainModels.TankActions;

namespace Tanks.Api.Json;

public static class JsonExtensions
{

    public static IMvcBuilder AddJsonOptions(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new TupleJsonConverter<int, int>());

            options.JsonSerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    JsonExtensions.AddDerivedTypes(typeof(ITankAction),
                    new []
                    {
                        typeof(MoveTankAction),
                        typeof(DealDamageTankAction)
                    })
                }
            };
        });

        return mvcBuilder;
    }


    private static Action<JsonTypeInfo> AddDerivedTypes(Type baseType, IEnumerable<Type> derivedTypes) => typeInfo =>
    {
        if (typeInfo.Kind != JsonTypeInfoKind.Object)
        {
            return;
        }

        if (baseType.IsAssignableFrom(typeInfo.Type))
        {
            typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "type",
            };
            foreach (var type in derivedTypes.Where(t => typeInfo.Type.IsAssignableFrom(t)))
            {
                typeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(type, type.Name));
            }
        }

    };

}