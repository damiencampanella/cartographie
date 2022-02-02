using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Flowly.Api.Map;
using BlazorFlowly.Models;

namespace BlazorFlowly.Services
{
    public interface IMapDataService
    {
        Task<ApiMap> GetApiMapDataAsync();
    }

    public class MapDataService : IMapDataService
    {
        private readonly HttpClient _httpClient;

        public MapDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiMap> GetApiMapDataAsync()
        {
            //return await _httpClient.GetFromJsonAsync<ApiMap>(Globals.TARGET_URL + "api/Map.ashx");
            return await GetApiMapData(Globals.TARGET_URL + "api/Map.ashx");
        }

        private async Task<ApiMap> GetApiMapData(string url)
        {
            ApiMap results;

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
                    results = Utf8Json.JsonSerializer.Deserialize<ApiMap>(contentString);
                    //results = System.Text.Json.JsonSerializer.Deserialize<ApiMap>(contentString);
                    //results = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiMap>(contentString); // comparable
                }
                catch (System.Exception) {

                    return null;
                }
                return results;
            }
        }
    }
}
