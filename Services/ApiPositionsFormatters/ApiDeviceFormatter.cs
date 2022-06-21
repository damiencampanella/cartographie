using Utf8Json;
using Flowly.Api.Positions;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class ApiDeviceFormatter : IJsonFormatter<ApiDevice> {
        private readonly byte[][] stringByteKeys;
        public ApiDeviceFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("DeviceId"), // {\"Title\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("State"), // ,\"RealTime\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiDevice device, IJsonFormatterResolver formatterResolver) {
            if (device == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[0]);
            writer.WriteString(device.DeviceId);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[1]);
            writer.WriteInt32(device.State);

            writer.WriteEndObject();
        }

        public ApiDevice Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;

            ApiDevice device = new ApiDevice();
            reader.AdvanceOffset(stringByteKeys[0].Length);
            device.DeviceId = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            device.State = reader.ReadInt32();

            reader.ReadIsEndObject();

            return device;
        }
    }
}
