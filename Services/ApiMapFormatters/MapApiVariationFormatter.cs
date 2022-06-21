using Utf8Json;
using Flowly.Api.Map;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class MapApiVariationFormatter : IJsonFormatter<ApiVariation> {

        private readonly byte[][] stringByteKeys;
        public MapApiVariationFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Id"), // {\"Id\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Description"), // ,\"Name\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Bounds"), // ,\"Latitude\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Dash"), // ,\"Longitude\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Display"), // ,\"Terminus\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Stations"), // ,\"Lines\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Shape"), // ,\"Lines\":
            };
        }


        public void Serialize (ref JsonWriter writer, ApiVariation variation, IJsonFormatterResolver formatterResolver) {
            if (variation == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw6(ref writer, stringByteKeys[0]);
            writer.WriteInt32(variation.Id);

            UnsafeMemory64.WriteRaw15(ref writer, stringByteKeys[1]);
            writer.WriteString(variation.Description);

            UnsafeMemory64.WriteRaw10(ref writer, stringByteKeys[2]);
            writer.WriteString(variation.Bounds);

            UnsafeMemory64.WriteRaw8(ref writer, stringByteKeys[3]);
            writer.WriteString(variation.Dash);

            UnsafeMemory64.WriteRaw11(ref writer, stringByteKeys[4]);
            writer.WriteBoolean(variation.Display);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[5]);
            formatterResolver.GetFormatterWithVerify<ApiStationRef[]>().Serialize(ref writer, variation.Stations, formatterResolver);

            UnsafeMemory64.WriteRaw9(ref writer, stringByteKeys[6]);
            formatterResolver.GetFormatterWithVerify<double[][]>().Serialize(ref writer, variation.Shape, formatterResolver);

            writer.WriteEndObject();
        }

        public ApiVariation Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;
            ApiVariation variation = new ApiVariation();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            variation.Id = reader.ReadInt32();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            variation.Description = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[2].Length);
            variation.Bounds = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[3].Length);
            variation.Dash = reader.ReadString();

            reader.AdvanceOffset(stringByteKeys[4].Length);
            variation.Display = reader.ReadBoolean();

            reader.AdvanceOffset(stringByteKeys[5].Length);
            variation.Stations = formatterResolver.GetFormatterWithVerify<ApiStationRef[]>().Deserialize(ref reader, formatterResolver);

            reader.AdvanceOffset(stringByteKeys[6].Length);
            variation.Shape = formatterResolver.GetFormatterWithVerify<double[][]>().Deserialize(ref reader, formatterResolver);

            reader.ReadIsEndObject();

            return variation;
        }
    }
}
