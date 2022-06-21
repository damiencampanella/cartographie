using Utf8Json;
using Flowly.Api.Map;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class MapApiStationFormatter : IJsonFormatter<ApiStation> {

        private readonly byte[][] stringByteKeys;
        public MapApiStationFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Id"), // {\"Id\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Name"), // ,\"Name\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Latitude"), // ,\"Latitude\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Longitude"), // ,\"Longitude\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Terminus"), // ,\"Terminus\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Lines"), // ,\"Lines\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiStation station, IJsonFormatterResolver formatterResolver) {
            if (station == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw6(ref writer, stringByteKeys[0]);
            writer.WriteString(station.Id);

            UnsafeMemory64.WriteRaw8(ref writer, stringByteKeys[1]);
            writer.WriteString(station.Name);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[2]);
            writer.WriteDouble(station.Latitude);

            UnsafeMemory64.WriteRaw13(ref writer, stringByteKeys[3]);
            writer.WriteDouble(station.Longitude);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[4]);
            writer.WriteBoolean(station.Terminus);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[5]);
            formatterResolver.GetFormatterWithVerify<ApiLineRef[]>().Serialize(ref writer, station.Lines, formatterResolver);
            
            writer.WriteEndObject();
        }

        public ApiStation Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;
            ApiStation station = new ApiStation();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            station.Id = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            station.Name = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[2].Length);
            station.Latitude = reader.ReadDouble();

            reader.AdvanceOffset(stringByteKeys[3].Length);
            station.Longitude = reader.ReadDouble();

            reader.AdvanceOffset(stringByteKeys[4].Length);
            station.Terminus = reader.ReadBoolean();

            reader.AdvanceOffset(stringByteKeys[5].Length);
            station.Lines = formatterResolver.GetFormatterWithVerify<ApiLineRef[]>().Deserialize(ref reader, formatterResolver);

            reader.ReadIsEndObject();

            return station;
        }
    }
}
