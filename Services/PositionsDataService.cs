using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Flowly.Api.Positions;
using BlazorFlowly.Models;

namespace BlazorFlowly.Services
{
    public interface IPositionsDataService
    {
        Task<ApiPositions> GetApiPositionsDataAsync();
    }

    public class PositionsDataService : IPositionsDataService
    {
        private readonly HttpClient _httpClient;

        public PositionsDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiPositions> GetApiPositionsDataAsync()
        {
            //return await _httpClient.GetFromJsonAsync<ApiPositions>(Globals.TARGET_URL + "api/Positions.ashx");
            return await GetApiPositionsData(Globals.TARGET_URL + "api/Positions.ashx");
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
                    results = Utf8Json.JsonSerializer.Deserialize<ApiPositions>(contentString);
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
