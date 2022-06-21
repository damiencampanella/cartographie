using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Flowly.Api.Positions;
using BlazorFlowly.Models;
using Utf8Json;

namespace BlazorFlowly.Services
{
    public interface IPositionsDataService
    {
        Task<ApiPositions> GetApiPositionsDataAsync(string url);
    }

    public class PositionsDataService : IPositionsDataService
    {
        private readonly HttpClient _httpClient;

        public PositionsDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiPositions> GetApiPositionsDataAsync(string url)
        {
            //return await _httpClient.GetFromJsonAsync<ApiPositions>(Globals.TARGET_URL + "api/Positions.ashx");
            return await GetApiPositionsData(url + "api/Positions.ashx");
        }
        private async Task<ApiPositions> GetApiPositionsData(string url)
        {
            ApiPositions results;

            Task<HttpResponseMessage> taskResponse = _httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead, default);

            using (HttpResponseMessage response = await taskResponse)
            {

                response.EnsureSuccessStatusCode();
                var content = response.Content!;

                if (content == null)
                {
                    throw new System.ArgumentNullException(nameof(content));
                }

                string contentString = await content.ReadAsStringAsync();

                try {
                    results = JsonSerializer.Deserialize<ApiPositions>(contentString);

                    // Serialization test
                    //JsonWriter writer = new JsonWriter();
                    //JsonSerializer.Serialize<ApiPositions>(ref writer, results, ApiPositionsResolver.Instance);
                    //JsonReader reader2 = new JsonReader(writer.ToUtf8ByteArray());
                    //ApiPositions positions = JsonSerializer.Deserialize<ApiPositions>(ref reader2, ApiPositionsResolver.Instance);

                    // Other deserializers
                    //results = System.Text.Json.JsonSerializer.Deserialize<ApiPositions>(contentString);
                    //results = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiPositions>(contentString); // comparable
                }
                catch (System.Exception) {

                    return null;
                }
                return results;
            }
        }
    }
}
