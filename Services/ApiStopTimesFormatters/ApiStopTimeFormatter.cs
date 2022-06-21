using Utf8Json;
using Flowly.Api.StopTimes;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class ApiStopTimeFormatter : IJsonFormatter<ApiStopTime> {
        private readonly byte[][] stringByteKeys;
        public ApiStopTimeFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("PassingTime"), // {\"Title\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("IsRealTime"), // ,\"RealTime\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Load"), // ,\"Load\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("CourseId"), // ,\"CourseId\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiStopTime stopTime, IJsonFormatterResolver formatterResolver) {
            if (stopTime == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw15(ref writer, stringByteKeys[0]);
            writer.WriteString(stopTime.PassingTime);

            UnsafeMemory64.WriteRaw14(ref writer, stringByteKeys[1]);
            writer.WriteBoolean(stopTime.IsRealTime);

            UnsafeMemory64.WriteRaw8(ref writer, stringByteKeys[2]);
            writer.WriteInt32(stopTime.Load);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[3]);
            writer.WriteString(stopTime.PassingTime);

            writer.WriteEndObject();
        }

        public ApiStopTime Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;

            ApiStopTime stopTime = new ApiStopTime();
            reader.AdvanceOffset(stringByteKeys[0].Length);
            stopTime.PassingTime = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            stopTime.IsRealTime = reader.ReadBoolean();

            reader.AdvanceOffset(stringByteKeys[2].Length);
            stopTime.Load = reader.ReadInt32();

            reader.AdvanceOffset(stringByteKeys[3].Length);
            stopTime.CourseId = reader.ReadString();

            reader.ReadIsEndObject();

            return stopTime;
        }
    }
}
