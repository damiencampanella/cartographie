using System.Collections.Generic;
using System;
using Utf8Json.Resolvers;
using Utf8Json;
using Flowly.Api.Positions;

namespace BlazorFlowly.Services {
    public class ApiPositionsResolver : IJsonFormatterResolver {
        public static ApiPositionsResolver Instance = new ApiPositionsResolver();
        public Dictionary<Type, IJsonFormatter> formatters;
        private ApiPositionsResolver () {
            formatters = new Dictionary<Type, IJsonFormatter>()
            {
            {typeof(ApiPositions), new ApiPositionsFormatter()},
            {typeof(ApiPosition), new ApiPositionFormatter()},
            {typeof(ApiLineRef), new PositionsApiLineRefFormatter()},
            {typeof(ApiStationRef), new PositionsApiStationRefFormatter()},
            {typeof(ApiRealTime), new ApiRealTimeFormatter()},
            {typeof(ApiDevice), new ApiDeviceFormatter()},
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
