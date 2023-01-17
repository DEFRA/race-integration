using System.Net.Http.Json;
using static RACE2.FrontEnd.Pages.FetchData;

namespace RACE2.FrontEnd.Services
{
    public class WeatherForecastService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public WeatherForecastService(
            IHttpClientFactory httpClientFactory)
            => this.httpClientFactory=httpClientFactory;
        public async ValueTask<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            HttpClient httpClient=this.httpClientFactory.CreateClient(nameof(WeatherForecastService));
            WeatherForecast[] result = await httpClient.GetFromJsonAsync<WeatherForecast[]>("weatherforecast");
            return result;
        }
     }
}
