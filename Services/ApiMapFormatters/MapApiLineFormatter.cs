using Utf8Json;
using Flowly.Api.Map;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class MapApiLineFormatter : IJsonFormatter<ApiLine> {

        private readonly byte[][] stringByteKeys;
        public MapApiLineFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Id"), // {\"Id\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Name"), // ,\"Name\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Color"), // ,\"Color\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("ForeColor"), // ,\"ForeColor\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Category"), // ,\"Category\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Weight"), // ,\"Weight\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Display"), // ,\"Display\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Roads"), // ,\"Roads\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiLine line, IJsonFormatterResolver formatterResolver) {
            if (line == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw6(ref writer, stringByteKeys[0]);
            writer.WriteString(line.Id);

            UnsafeMemory64.WriteRaw8(ref writer, stringByteKeys[1]);
            writer.WriteString(line.Name);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[2]);
            writer.WriteString(line.Color);

            UnsafeMemory64.WriteRaw13(ref writer, stringByteKeys[3]);
            writer.WriteString(line.ForeColor);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[4]);
            writer.WriteString(line.Category);

            UnsafeMemory64.WriteRaw10(ref writer, stringByteKeys[5]);
            writer.WriteInt32(line.Weight);

            UnsafeMemory64.WriteRaw11(ref writer, stringByteKeys[6]);
            writer.WriteBoolean(line.Display);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[7]);
            formatterResolver.GetFormatterWithVerify<ApiRoad[]>().Serialize(ref writer, line.Roads, formatterResolver);

            writer.WriteEndObject();
        }

        public ApiLine Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;
            ApiLine line = new ApiLine();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            line.Id = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            line.Name = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[2].Length);
            line.Color = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[3].Length);
            line.ForeColor = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[4].Length);
            line.Category = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[5].Length);
            line.Weight = reader.ReadInt32();

            reader.AdvanceOffset(stringByteKeys[6].Length);
            line.Display = reader.ReadBoolean();

            reader.AdvanceOffset(stringByteKeys[7].Length);
            line.Roads = formatterResolver.GetFormatterWithVerify<ApiRoad[]>().Deserialize(ref reader, formatterResolver);

            reader.ReadIsEndObject();

            return line;
        }
    }
}
