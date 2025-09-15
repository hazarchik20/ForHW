using BLL.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL
{
    public class WeatherApiService : IWeatherApiSerice
    {
        private readonly IConfiguration _configuration;
        public WeatherApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public async Task<WeatherData> GetCurrentWeatherAsync(double lat, double lon)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/forecast");
            var response = await client.GetAsync($"?lat={lat}&lon={lon}&appid={_configuration["OPENWEATHER_API_KEY"]}");

            //client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
            //var response = await client.GetAsync($"?q={"London"}&units=metric&appid={_configuration["OPENWEATHER_API_KEY"]}");
            var json = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherData>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return weatherData ?? throw new ArgumentException();
        }
    }
}

