using Utf8Json;
using Flowly.Api.Positions;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class ApiPositionFormatter : IJsonFormatter<ApiPosition> {
        private readonly byte[][] stringByteKeys;
        public ApiPositionFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Devices"), // {\"Devices\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Vehicle"), // ,\"Vehicle\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("VehicleId"), // ,\"VehicleId\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Latitude"), // ,\"Latitude\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Longitude"), // ,\"Longitude\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("LastUpdate"), // ,\"LastUpdate\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Load"), // ,\"Load\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("State"), // ,\"State\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Icon"), // ,\"Icon\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("RealTime"), // ,\"Icon\":
            };
        }

        public void Serialize (ref JsonWriter writer, ApiPosition position, IJsonFormatterResolver formatterResolver) {
            if (position == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw11(ref writer, stringByteKeys[0]);
            formatterResolver.GetFormatterWithVerify<ApiDevice[]>().Serialize(ref writer, position.Devices, formatterResolver);

            UnsafeMemory64.WriteRaw11(ref writer, stringByteKeys[1]);
            writer.WriteString(position.Vehicle);

            UnsafeMemory64.WriteRaw13(ref writer, stringByteKeys[2]);
            if (position.VehicleId.HasValue) {
                writer.WriteInt32(position.VehicleId.Value);
            }
            else {
                writer.WriteNull();
            }

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[3]);
            writer.WriteDouble(position.Latitude);

            UnsafeMemory64.WriteRaw13(ref writer, stringByteKeys[4]);
            writer.WriteDouble(position.Longitude);

            UnsafeMemory64.WriteRaw14(ref writer, stringByteKeys[5]);
            writer.WriteString(position.LastUpdate);

            UnsafeMemory64.WriteRaw8(ref writer, stringByteKeys[6]);
            writer.WriteInt32(position.Load);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[7]);
            writer.WriteInt32(position.State);

            UnsafeMemory64.WriteRaw8(ref writer, stringByteKeys[8]);
            writer.WriteString(position.Icon);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[9]);
            formatterResolver.GetFormatterWithVerify<ApiRealTime>().Serialize(ref writer, position.RealTime, formatterResolver);

            writer.WriteEndObject();
        }

        public ApiPosition Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;

            ApiPosition position = new ApiPosition();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            position.Devices = formatterResolver.GetFormatterWithVerify<ApiDevice[]>().Deserialize(ref reader, formatterResolver);

            reader.AdvanceOffset(stringByteKeys[1].Length);
            position.Vehicle = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[2].Length);
            if (reader.ReadIsNull()) {
                position.VehicleId = null;
            }
            else {
                position.VehicleId = reader.ReadInt32();
            }

            reader.AdvanceOffset(stringByteKeys[3].Length);
            position.Latitude = reader.ReadDouble();

            reader.AdvanceOffset(stringByteKeys[4].Length);
            position.Longitude = reader.ReadDouble();

            reader.AdvanceOffset(stringByteKeys[5].Length);
            position.LastUpdate = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[6].Length);
            position.Load = reader.ReadInt32();

            reader.AdvanceOffset(stringByteKeys[7].Length);
            position.State = reader.ReadInt32();

            reader.AdvanceOffset(stringByteKeys[8].Length);
            position.Icon = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[9].Length);
            position.RealTime = formatterResolver.GetFormatterWithVerify<ApiRealTime>().Deserialize(ref reader, formatterResolver);

            reader.ReadIsEndObject();

            return position;
        }
    }
}
