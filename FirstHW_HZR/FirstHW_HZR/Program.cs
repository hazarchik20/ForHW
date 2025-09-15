using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;



namespace FirstHW_HZR
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Random rnd = new Random();
            var httpClient = new HttpClient();
            string uri = "https://pokeapi.co/api/v2/";
            httpClient.BaseAddress = new Uri(uri);
            var response = await httpClient.GetAsync("pokemon/"+rnd.Next(1, 20)+"/");
            var content = await response.Content.ReadAsStringAsync();
            var pokemon = JsonSerializer.Deserialize<Pokemon>(content);
            Console.WriteLine($"id - {pokemon.id}\nname - {pokemon.name}\nheight - {pokemon.height}\nspecies: \n\tname - {pokemon.species.name}\nstats:");
            for (int i = 0; i < pokemon.stats.Count; i++)
            {
                Console.WriteLine($"\t[{i}] \n\t\tbase_stat - {pokemon.stats[i].base_stat} \n\t\teffort - {pokemon.stats[i].effort} \n\t\tstat: \n\t\t\tname - {pokemon.stats[i].stat.name}");
            }
            for (int i = 0; i < pokemon.held_items.Length; i++)
            {
                Console.WriteLine($"held_items: \n\titem[{i}] - \n\t\tname {pokemon.held_items[i].item.name}\n\t\turl {pokemon.held_items[i].item.url}");
            }
            if (pokemon.held_items.Length != 0)
            {
                string str = pokemon.held_items[0].item.url.Replace(uri, "");
                response = await httpClient.GetAsync(str);
                content = await response.Content.ReadAsStringAsync();
                var ItemAtridute = System.Text.Json.JsonSerializer.Deserialize<ItemAtridute>(content);
                Console.WriteLine($"Id - {ItemAtridute.id}\nName - {ItemAtridute.name}\nCost - {ItemAtridute.cost}");
            }
            Console.ReadLine();

        }
    }
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("height")]
        public int height { get; set; }

        [JsonPropertyName("species")]
        public Species species { get; set; }

        [JsonPropertyName("stats")]
        public List<Pokemon_stats> stats { get; set; }

        [JsonPropertyName("held_items")]
        public Pokemon_held_items[] held_items { get; set; }
    }
    public class Pokemon_stats
    {
        [JsonPropertyName("base_stat")]
        public int base_stat { get; set; }
        [JsonPropertyName("effort")]
        public int effort { get; set; }
        [JsonPropertyName("stat")]
        public Stat stat{ get; set; }
    }
    public class Stat
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
    }
    public class Pokemon_held_items
    {
        [JsonPropertyName("item")]
        public Pokemon_items item { get; set; }
    }
    public class Species
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
    }
    public class Pokemon_items
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("url")]
        public string url { get; set; }
    }
    public class ItemAtridute
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("id")]
        public int id { get; set; }
        [JsonPropertyName("cost")]
        public int cost { get; set; }

    }
}

