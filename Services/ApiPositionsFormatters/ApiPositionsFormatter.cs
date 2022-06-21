using Utf8Json;
using Flowly.Api.Positions;
using Utf8Json.Internal;
using System;

namespace BlazorFlowly.Services {
    public class ApiPositionsFormatter : IJsonFormatter<ApiPositions> {
        private readonly byte[][] stringByteKeys;
        public ApiPositionsFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("ServerTime"), // {\"ServerTime\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Positions"), // ,\"Positions\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiPositions positions, IJsonFormatterResolver formatterResolver) {
            if (positions == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw14(ref writer, stringByteKeys[0]);
            writer.WriteString(positions.ServerTime);

            UnsafeMemory64.WriteRaw13(ref writer, stringByteKeys[1]);
            formatterResolver.GetFormatterWithVerify<ApiPosition[]>().Serialize(ref writer, positions.Positions, formatterResolver);

            writer.WriteEndObject();
        }

        public ApiPositions Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;

            ApiPositions positions = new ApiPositions();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            positions.ServerTime = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            positions.Positions = formatterResolver.GetFormatterWithVerify<ApiPosition[]>().Deserialize(ref reader, formatterResolver);

            reader.ReadIsEndObject();

            return positions;
        }
    }
}
