using System;
using Utf8Json;
using Flowly.Api.Map;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class ApiMapFormatter : IJsonFormatter<ApiMap> {
        private readonly byte[][] stringByteKeys;
        public ApiMapFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Title"), // {\"Title\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Location"), // ,\"Location\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Date"), // ,\"Date\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Bounds"), // ,\"Bounds\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Stations"), // ,\"Stations\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Lines"), // ,\"Lines\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Times"), // ,\"Times\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("RealTime"), // ,\"RealTime\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Load"), // ,\"Load\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiMap map, IJsonFormatterResolver formatterResolver) {
            if (map == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[0]);
            writer.WriteString(map.Title);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[1]);
            writer.WriteString(map.Location);

            UnsafeMemory64.WriteRaw8(ref writer, stringByteKeys[2]);
            formatterResolver.GetFormatterWithVerify<DateTime?>().Serialize(ref writer, map.Date, formatterResolver);

            UnsafeMemory64.WriteRaw10(ref writer, stringByteKeys[3]);
            writer.WriteString(map.Bounds);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[4]);
            formatterResolver.GetFormatterWithVerify<ApiStation[]>().Serialize(ref writer, map.Stations, formatterResolver);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[5]);
            formatterResolver.GetFormatterWithVerify<ApiLine[]>().Serialize(ref writer, map.Lines, formatterResolver);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[6]);
            writer.WriteBoolean(map.Times);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[7]);
            writer.WriteBoolean(map.RealTime);

            UnsafeMemory64.WriteRaw8(ref writer, stringByteKeys[8]);
            writer.WriteBoolean(map.Load);

            writer.WriteEndObject();
        }

        public ApiMap Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;

            ApiMap map = new ApiMap();
            reader.AdvanceOffset(stringByteKeys[0].Length);
            map.Title = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            map.Location = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[2].Length);
            map.Date = formatterResolver.GetFormatterWithVerify<DateTime?>().Deserialize(ref reader, formatterResolver);

            reader.AdvanceOffset(stringByteKeys[3].Length);
            map.Bounds = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[4].Length);
            map.Stations = formatterResolver.GetFormatterWithVerify<ApiStation[]>().Deserialize(ref reader, formatterResolver);

            reader.AdvanceOffset(stringByteKeys[5].Length);
            map.Lines = formatterResolver.GetFormatterWithVerify<ApiLine[]>().Deserialize(ref reader, formatterResolver);

            reader.AdvanceOffset(stringByteKeys[6].Length);
            map.Times = reader.ReadBoolean();

            reader.AdvanceOffset(stringByteKeys[7].Length);
            map.RealTime = reader.ReadBoolean();

            reader.AdvanceOffset(stringByteKeys[8].Length);
            map.Load = reader.ReadBoolean();

            reader.ReadIsEndObject();

            return map;
        }
    }
}
