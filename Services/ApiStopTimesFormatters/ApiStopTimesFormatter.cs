using Utf8Json;
using Flowly.Api.StopTimes;
using Utf8Json.Internal;
using System;

namespace BlazorFlowly.Services {
    public class ApiStopTimesFormatter : IJsonFormatter<ApiStopTimes> {
        private readonly byte[][] stringByteKeys;
        public ApiStopTimesFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("ServerTime"), // {\"ServerTime\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Lines"), // ,\"Positions\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiStopTimes stopTimes, IJsonFormatterResolver formatterResolver) {
            if (stopTimes == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw14(ref writer, stringByteKeys[0]);
            writer.WriteString(stopTimes.ServerTime);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[1]);
            formatterResolver.GetFormatterWithVerify<ApiLine[]>().Serialize(ref writer, stopTimes.Lines, formatterResolver);

            writer.WriteEndObject();
        }

        public ApiStopTimes Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;

            ApiStopTimes stopTimes = new ApiStopTimes();
            reader.AdvanceOffset(stringByteKeys[0].Length);
            stopTimes.ServerTime = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            stopTimes.Lines = formatterResolver.GetFormatterWithVerify<ApiLine[]>().Deserialize(ref reader, formatterResolver);

            reader.ReadIsEndObject();

            return stopTimes;
        }
    }
}
