using System.Collections.Generic;
using System;
using Utf8Json.Resolvers;
using Utf8Json;
using Flowly.Api.Map;

namespace BlazorFlowly.Services {
    public class ApiMapResolver : IJsonFormatterResolver {
        public static ApiMapResolver Instance = new ApiMapResolver();
        public Dictionary<Type, IJsonFormatter> formatters;
        private ApiMapResolver () {
            formatters = new Dictionary<Type, IJsonFormatter>()
            {
            {typeof(ApiMap), new ApiMapFormatter()},
            {typeof(ApiLine), new MapApiLineFormatter()},
            {typeof(ApiLineRef), new MapApiLineRefFormatter()},
            {typeof(ApiRoad), new MapApiRoadFormatter()},
            {typeof(ApiVariation), new MapApiVariationFormatter()},
            {typeof(ApiStation), new MapApiStationFormatter()},
            {typeof(ApiStationRef), new MapApiStationRefFormatter()},
        };
        }

        public IJsonFormatter<T> GetFormatter<T> () {
            if (formatters.TryGetValue(typeof(T), out var typeFormatter)) {
                return (IJsonFormatter<T>)typeFormatter;
            }

            return StandardResolver.Default.GetFormatter<T>();
        }
    }
}
