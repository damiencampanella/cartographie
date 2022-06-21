using Utf8Json;
using Flowly.Api.Positions;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class ApiRealTimeFormatter : IJsonFormatter<ApiRealTime> {

        private readonly byte[][] stringByteKeys;
        public ApiRealTimeFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Line"), // {\"Id\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Direction"), // ,\"Name\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("VariationId"), // ,\"Color\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("CourseId"), // ,\"ForeColor\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Station"), // ,\"Category\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("LastUpdate"), // ,\"Weight\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Delay"), // ,\"Display\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiRealTime realTime, IJsonFormatterResolver formatterResolver) {
            if (realTime == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw8(ref writer, stringByteKeys[0]);
            formatterResolver.GetFormatterWithVerify<ApiLineRef>().Serialize(ref writer, realTime.Line, formatterResolver);

            UnsafeMemory64.WriteRaw13(ref writer, stringByteKeys[1]);
            writer.WriteInt32(realTime.Direction);

            UnsafeMemory64.WriteRaw15(ref writer, stringByteKeys[2]);
            writer.WriteInt32(realTime.VariationId);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[3]);
            writer.WriteString(realTime.CourseId);

            UnsafeMemory64.WriteRaw11(ref writer, stringByteKeys[4]);
            formatterResolver.GetFormatterWithVerify<ApiStationRef>().Serialize(ref writer, realTime.Station, formatterResolver);

            UnsafeMemory64.WriteRaw14(ref writer, stringByteKeys[5]);
            writer.WriteString(realTime.LastUpdate);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[6]);
            writer.WriteInt32(realTime.Delay);

            writer.WriteEndObject();
        }

        public ApiRealTime Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;
            ApiRealTime realTime = new ApiRealTime();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            realTime.Line = formatterResolver.GetFormatterWithVerify<ApiLineRef>().Deserialize(ref reader, formatterResolver);

            reader.AdvanceOffset(stringByteKeys[1].Length);
            realTime.Direction = reader.ReadInt32();

            reader.AdvanceOffset(stringByteKeys[2].Length);
            realTime.VariationId = reader.ReadInt32();

            reader.AdvanceOffset(stringByteKeys[3].Length);
            realTime.CourseId = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[4].Length);
            realTime.Station = formatterResolver.GetFormatterWithVerify<ApiStationRef>().Deserialize(ref reader, formatterResolver);

            reader.AdvanceOffset(stringByteKeys[5].Length);
            realTime.LastUpdate = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[6].Length);
            realTime.Delay = reader.ReadInt32();

            reader.ReadIsEndObject();

            return realTime;
        }
    }
}
