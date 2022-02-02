using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Flowly.Api.StopTimes;
using BlazorFlowly.Models;

namespace BlazorFlowly.Services
{
    public interface IStopTimesDataService
    {
        Task<ApiStopTimes> GetApiStopTimesDataAsync();
    }

    public class StopTimesDataService : IStopTimesDataService
    {
        private readonly HttpClient _httpClient;

        public StopTimesDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiStopTimes> GetApiStopTimesDataAsync()
        {
            //return await _httpClient.GetFromJsonAsync<ApiStopTimes>(Globals.TARGET_URL + "api/StopTimes.ashx");
            return await GetApiStopTimesData(Globals.TARGET_URL + "api/StopTimes.ashx");

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

                string contentString = await content.ReadAsStringAsync();

                try {
                    results = Utf8Json.JsonSerializer.Deserialize<ApiStopTimes>(contentString);
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
