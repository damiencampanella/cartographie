using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Flowly.Api.StopTimes;
using BlazorFlowly.Models;
using Utf8Json;

namespace BlazorFlowly.Services
{
    public interface IStopTimesDataService
    {
        Task<ApiStopTimes> GetApiStopTimesDataAsync(string url);
    }

    public class StopTimesDataService : IStopTimesDataService
    {
        private readonly HttpClient _httpClient;

        public StopTimesDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiStopTimes> GetApiStopTimesDataAsync(string url)
        {
            //return await _httpClient.GetFromJsonAsync<ApiStopTimes>(Globals.TARGET_URL + "api/StopTimes.ashx");
            return await GetApiStopTimesData(url + "api/StopTimes.ashx");

        }
        private async Task<ApiStopTimes> GetApiStopTimesData(string url)
        {
            ApiStopTimes results;

            Task<HttpResponseMessage> taskResponse = _httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead, default);

            using (HttpResponseMessage response = await taskResponse)
            {

                response.EnsureSuccessStatusCode();
                var content = response.Content!;

                if (content == null)
                {
                    throw new System.ArgumentNullException(nameof(content));
                }

                //string contentString = await content.ReadAsStringAsync();
                byte[] contentBytes = await content.ReadAsByteArrayAsync();

                try {
                    JsonReader reader = new JsonReader(contentBytes);
                    results = JsonSerializer.Deserialize<ApiStopTimes>(ref reader, ApiStopTimesResolver.Instance);

                    // Serialization test
                    //JsonWriter writer = new JsonWriter();
                    //JsonSerializer.Serialize<ApiStopTimes>(ref writer, results, ApiStopTimesResolver.Instance);
                    //JsonReader reader2 = new JsonReader(writer.ToUtf8ByteArray());
                    //ApiStopTimes stopTimes = JsonSerializer.Deserialize<ApiStopTimes>(ref reader2, ApiStopTimesResolver.Instance);

                    // Other deserializers
                    //results = Utf8Json.JsonSerializer.Deserialize<ApiStopTimes>(contentString);
                    //results = System.Text.Json.JsonSerializer.Deserialize<ApiStopTimes>(contentString);
                    //results = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiStopTimes>(contentString); // comparable
                }
                catch (System.Exception) {
                    return null;
                }
                return results;
            }
        }
    }
}
