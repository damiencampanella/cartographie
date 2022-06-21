using System.Collections.Generic;
using System;
using Utf8Json.Resolvers;
using Utf8Json;
using Flowly.Api.StopTimes;

namespace BlazorFlowly.Services {
    public class ApiStopTimesResolver : IJsonFormatterResolver {
        public static ApiStopTimesResolver Instance = new ApiStopTimesResolver();
        public Dictionary<Type, IJsonFormatter> formatters;
        private ApiStopTimesResolver () {
            formatters = new Dictionary<Type, IJsonFormatter>()
            {
            {typeof(ApiStopTimes), new ApiStopTimesFormatter()},
            {typeof(ApiLine), new StopTimesApiLineFormatter()},
            {typeof(ApiStopTime), new ApiStopTimeFormatter()},
            {typeof(ApiRoad), new StopTimesApiRoadFormatter()},
            {typeof(ApiVariation), new StopTimesApiVariationFormatter()},
            {typeof(ApiStation), new StopTimesApiStationFormatter()},
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
