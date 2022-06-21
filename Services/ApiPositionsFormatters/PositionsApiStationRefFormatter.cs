using Utf8Json;
using Flowly.Api.Positions;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class PositionsApiStationRefFormatter : IJsonFormatter<ApiStationRef> {

        private readonly byte[][] stringByteKeys;
        public PositionsApiStationRefFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Id"), // {\"Id\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiStationRef station, IJsonFormatterResolver formatterResolver) {
            if (station == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw6(ref writer, stringByteKeys[0]);
            writer.WriteString(station.Id);

            writer.WriteEndObject();
        }

        public ApiStationRef Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;

            ApiStationRef station = new ApiStationRef();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            station.Id = reader.ReadString();

            reader.ReadIsEndObject();

            return station;
        }
    }
}
