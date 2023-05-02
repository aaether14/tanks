namespace Tanks.Infrastructure.Serializers;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

public class CustomTupleSerializer<T1, T2> : SerializerBase<(T1, T2)>
{

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, (T1, T2) value)
    {
        var document = new BsonDocument
        {
            { "Item1", BsonValue.Create(value.Item1) },
            { "Item2", BsonValue.Create(value.Item2) }
        };

        BsonSerializer.Serialize(context.Writer, document);
    }

    public override (T1, T2) Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var document = BsonSerializer.Deserialize<BsonDocument>(context.Reader);

        T1 item1 = (T1) BsonTypeMapper.MapToDotNetValue(document["Item1"]);
        T2 item2 = (T2) BsonTypeMapper.MapToDotNetValue(document["Item2"]);

        return (item1, item2);
    }
}
