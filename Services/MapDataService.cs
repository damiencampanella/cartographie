using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Flowly.Api.Map;
using BlazorFlowly.Models;
using System.Collections;
using Utf8Json;

namespace BlazorFlowly.Services
{
    public interface IMapDataService
    {
        Task<ApiMap> GetApiMapDataAsync(string url);
    }

    public class MapDataService : IMapDataService
    {
        private readonly HttpClient _httpClient;

        public MapDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiMap> GetApiMapDataAsync(string url)
        {
            //return await _httpClient.GetFromJsonAsync<ApiMap>(Globals.TARGET_URL + "api/Map.ashx");
            return await GetApiMapData(url + "api/Map.ashx");
        }

        private async Task<ApiMap> GetApiMapData(string url)
        {
            ApiMap results;

            Task<HttpResponseMessage> taskResponse = _httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead, default);

            using HttpResponseMessage response = await taskResponse;

            response.EnsureSuccessStatusCode();
            var content = response.Content!;

            if (content == null) {
                throw new System.ArgumentNullException(nameof(content));
            }

            //string contentString = await content.ReadAsStringAsync();
            byte[] contentBytes = await content.ReadAsByteArrayAsync();
            //string dateString = "\"2022-02-17T10:19:23.337+01:00\"";
            //string dateString2 = "\"2022-04-11T11:52:11.3820000+04:00\"";

            try {
                JsonReader reader = new JsonReader(contentBytes);
                results = JsonSerializer.Deserialize<ApiMap>(ref reader, ApiMapResolver.Instance);

                // Serialization test
                //JsonWriter writer = new JsonWriter();
                //JsonSerializer.Serialize<ApiMap>(ref writer, results, ApiMapResolver.Instance);
                //JsonReader reader2 = new JsonReader(writer.ToUtf8ByteArray());
                //ApiMap map = JsonSerializer.Deserialize<ApiMap>(ref reader2, ApiMapResolver.Instance);

                // Other deserializers
                //results = Utf8Json.JsonSerializer.Deserialize<ApiMap>(contentString);
                //results = System.Text.Json.JsonSerializer.Deserialize<ApiMap>(contentString);
                //results = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiMap>(contentString); // comparable

                // DateTime tests
                //byte[] bytes = Utf8Json.JsonSerializer.Serialize<DateTime>(System.DateTime.Now);
                //string result = System.Text.Encoding.UTF8.GetString(bytes);
                //DateTime date = Utf8Json.JsonSerializer.Deserialize<DateTime>(result);
                //DateTime date2 = Utf8Json.JsonSerializer.Deserialize<DateTime>(dateString);
                //DateTime date3 = Utf8Json.JsonSerializer.Deserialize<DateTime>(dateString2);
            }
            catch (System.Exception) {

                return null;
            }
            return results;
        }
    }
}
