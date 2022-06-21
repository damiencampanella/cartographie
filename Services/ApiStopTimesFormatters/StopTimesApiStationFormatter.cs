using Utf8Json;
using Flowly.Api.StopTimes;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class StopTimesApiStationFormatter : IJsonFormatter<ApiStation> {

        private readonly byte[][] stringByteKeys;
        public StopTimesApiStationFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Id"), // {\"Id\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("StopTimes"), // ,\"Name\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiStation station, IJsonFormatterResolver formatterResolver) {
            if (station == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw6(ref writer, stringByteKeys[0]);
            writer.WriteString(station.Id);

            UnsafeMemory64.WriteRaw13(ref writer, stringByteKeys[1]);
            formatterResolver.GetFormatterWithVerify<ApiStopTime[]>().Serialize(ref writer, station.StopTimes, formatterResolver);
            
            writer.WriteEndObject();
        }

        public ApiStation Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;
            ApiStation station = new ApiStation();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            station.Id = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            station.StopTimes = formatterResolver.GetFormatterWithVerify<ApiStopTime[]>().Deserialize(ref reader, formatterResolver);

            reader.ReadIsEndObject();

            return station;
        }
    }
}
