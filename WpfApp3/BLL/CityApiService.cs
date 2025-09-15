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
    public class CityApiService : ICityApiService
    {
        private readonly IConfiguration _configuration;
        public CityApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<City[]> GetCurrentCityAsync(string r)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://restcountries.com/v3.1/region/{r}");
            var response = await client.GetAsync($"");
            var json = await response.Content.ReadAsStringAsync();
            var list = JsonSerializer.Deserialize<City[]>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return list ?? throw new ArgumentException();
        }
    }
}
