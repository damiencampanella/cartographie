using Utf8Json;
using Flowly.Api.StopTimes;
using Utf8Json.Internal;

namespace BlazorFlowly.Services {
    public class StopTimesApiVariationFormatter : IJsonFormatter<ApiVariation> {

        private readonly byte[][] stringByteKeys;
        public StopTimesApiVariationFormatter () {
            // pre-encoded escaped string byte with "{", ":" and ",".
            stringByteKeys = new byte[][]
            {
            JsonWriter.GetEncodedPropertyNameWithBeginObject("Id"), // {\"Id\":
            JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Stations"), // ,\"Lines\":
            };
        }


        public void Serialize (ref JsonWriter writer, ApiVariation variation, IJsonFormatterResolver formatterResolver) {
            if (variation == null) { writer.WriteNull(); return; }

            UnsafeMemory64.WriteRaw6(ref writer, stringByteKeys[0]);
            writer.WriteInt32(variation.Id);

            UnsafeMemory64.WriteRaw12(ref writer, stringByteKeys[1]);
            formatterResolver.GetFormatterWithVerify<ApiStation[]>().Serialize(ref writer, variation.Stations, formatterResolver);

            writer.WriteEndObject();
        }

        public ApiVariation Deserialize (ref JsonReader reader, IJsonFormatterResolver formatterResolver) {
            if (reader.ReadIsNull()) return null;
            ApiVariation variation = new ApiVariation();

            reader.AdvanceOffset(stringByteKeys[0].Length);
            variation.Id = reader.ReadInt32();

            reader.AdvanceOffset(stringByteKeys[1].Length);
            variation.Stations = formatterResolver.GetFormatterWithVerify<ApiStation[]>().Deserialize(ref reader, formatterResolver);

            reader.ReadIsEndObject();

            return variation;
        }
    }
}
