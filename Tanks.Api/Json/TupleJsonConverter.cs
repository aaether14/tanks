using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tanks.Api.Json;

public class TupleJsonConverter<T1, T2> : JsonConverter<(T1, T2)>
{
    public override (T1, T2) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, (T1, T2) value, JsonSerializerOptions options)
    {
        writer.WriteStringValue($"({value.Item1}, {value.Item2})");
    }
}
