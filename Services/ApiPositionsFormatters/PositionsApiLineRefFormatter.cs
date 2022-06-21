using Utf8Json;
using Flowly.Api.Positions;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class PositionsApiLineRefFormatter : IJsonFormatter<ApiLineRef> {
        private readonly byte[][] stringByteKeys;
        public PositionsApiLineRefFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Id"), // {\"Id\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiLineRef line, IJsonFormatterResolver formatterResolver) {
            if (line == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw6(ref writer, stringByteKeys[0]);
            writer.WriteString(line.Id);

            writer.WriteEndObject();
        }

        public ApiLineRef Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;
            ApiLineRef line = new ApiLineRef();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            line.Id = reader.ReadString();

            reader.ReadIsEndObject();

            return line;
        }
    }
}
