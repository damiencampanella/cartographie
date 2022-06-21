using Utf8Json;
using Flowly.Api.StopTimes;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class StopTimesApiRoadFormatter : IJsonFormatter<ApiRoad> {

        private readonly byte[][] stringByteKeys;
        public StopTimesApiRoadFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            this.stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Direction"), // {\"Direction\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Variations"), // ,\"Variations\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiRoad road, IJsonFormatterResolver formatterResolver) {
            if (road == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw13(ref writer, stringByteKeys[0]);
            writer.WriteInt32(road.Direction);

            UnsafeMemory64.WriteRaw14(ref writer, stringByteKeys[1]);
            formatterResolver.GetFormatterWithVerify<ApiVariation[]>().Serialize(ref writer, road.Variations, formatterResolver);

            writer.WriteEndObject();
        }

        public ApiRoad Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;
            ApiRoad road = new ApiRoad();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            road.Direction = reader.ReadInt32();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            road.Variations = formatterResolver.GetFormatterWithVerify<ApiVariation[]>().Deserialize(ref reader, formatterResolver);

            reader.ReadIsEndObject();

            return road;
        }
    }
}
