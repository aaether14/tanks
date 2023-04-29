using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

public class TwoDimensionalIntArraySerializer : IBsonSerializer<int[,]>
{
    public Type ValueType => typeof(int[,]);

    public int[,] Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        BsonArray bsonArray = BsonArraySerializer.Instance.Deserialize(context);
        int[,] array = new int[bsonArray.Count, bsonArray[0].AsBsonArray.Count];

        for (int i = 0; i < bsonArray.Count; i++)
        {
            var bsonRow = bsonArray[i].AsBsonArray;
            for (int j = 0; j < bsonRow.Count; j++)
            {
                array[i, j] = bsonRow[j].AsInt32;
            }
        }

        return array;
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, int[,] value)
    {
        var bsonArray = new BsonArray(value.GetLength(0));

        for (int i = 0; i < value.GetLength(0); i++)
        {
            var bsonRow = new BsonArray(value.GetLength(1));
            for (int j = 0; j < value.GetLength(1); j++)
            {
                bsonRow.Add(value[i, j]);
            }
            bsonArray.Add(bsonRow);
        }

        BsonArraySerializer.Instance.Serialize(context, bsonArray);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        Serialize(context, args, (int[,])value);
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Deserialize(context, args);
    }
}
