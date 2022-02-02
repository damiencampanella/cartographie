using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorFlowly.Models;

namespace BlazorFlowly.Services
{
    public interface IChartService
    {
        Task<ChartDataItem[]> GetVisitDataAsync();
        Task<ChartDataItem[]> GetSalesDataAsync();
        Task<VisitorsDataItem[]> GetVisitorsDataAsync();
    }

    public class ChartService : IChartService
    {
        private readonly HttpClient _httpClient;

        public ChartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChartDataItem[]> GetVisitDataAsync()
        {
            return (await GetChartDataAsync()).VisitData;
        }

        public async Task<VisitorsDataItem[]> GetVisitorsDataAsync()
        {
            return (await GetChartDataAsync()).VisitorsData;
        }

        public async Task<ChartDataItem[]> GetSalesDataAsync()
        {
            return (await GetChartDataAsync()).SalesData;
        }

        private async Task<ChartData> GetChartDataAsync()
        {
            return await _httpClient.GetFromJsonAsync<ChartData>("data/fake_chart_data.json");
        }
    }
}
