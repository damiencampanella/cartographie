using Utf8Json;
using Flowly.Api.StopTimes;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class StopTimesApiLineFormatter : IJsonFormatter<ApiLine> {

        private readonly byte[][] stringByteKeys;
        public StopTimesApiLineFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Id"), // {\"Id\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Roads"), // ,\"Roads\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiLine line, IJsonFormatterResolver formatterResolver) {
            if (line == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw6(ref writer, stringByteKeys[0]);
            writer.WriteString(line.Id);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[1]);
            formatterResolver.GetFormatterWithVerify<ApiRoad[]>().Serialize(ref writer, line.Roads, formatterResolver);

            writer.WriteEndObject();
        }

        public ApiLine Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;
            ApiLine line = new ApiLine();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            line.Id = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            line.Roads = formatterResolver.GetFormatterWithVerify<ApiRoad[]>().Deserialize(ref reader, formatterResolver);

            reader.ReadIsEndObject();

            return line;
        }
    }
}
