using DLL;
using DLL.Models;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

var options = new DbContextOptionsBuilder<MusicContext>()
           .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MusicApiDB;Integrated Security=True;")
           .Options;
var context = new MusicContext(options);
var _repository = new MusicRepository(context);

var url = "http://localhost:12000/";

var listener = new HttpListener();
listener.Prefixes.Add(url);
listener.Start();

Console.WriteLine("Server started...");

while (true)
{
    var listenerContext = await listener.GetContextAsync();
    Task.Run(() => HandleAsync(listenerContext));
}

async Task HandleAsync(HttpListenerContext context)
{
    var endpoint = context.Request.Url?.AbsolutePath;
    var response = string.Empty;
    var statusCode = 0;

    if (endpoint.StartsWith("/music"))
    {
        (response, statusCode) = await HandlemusicEndpoint(context.Request);
    }

    var bytes = Encoding.UTF8.GetBytes(response);
    context.Response.ContentLength64 = bytes.Length;
    context.Response.StatusCode = statusCode;
    context.Response.ContentType = "application/json";
    await context.Response.OutputStream.WriteAsync(bytes, 0, bytes.Length);
    context.Response.OutputStream.Close();
}
async Task<(string, int)> HandlemusicEndpoint(HttpListenerRequest request)
{
    var context = new MusicContext();
    var repository = new MusicRepository(context);
    var url = request.Url?.AbsolutePath;
    var response = string.Empty;
    var statusCode = 0;

    if (request.HttpMethod == "GET")
    {
        var regexMatch = Regex.Match(url, @"^/music/(\d+)$");
        var regexSYMBLE_CURRENCYMatch = Regex.Match(url, @"^tracks\?offset=([A-Za-z]+)&limit=([A-Za-z]+)&name=([A-Za-z]+)$");
        if (regexMatch.Success)
        {
            var id = int.Parse(regexMatch.Groups[1].Value);
            var music = await repository.SearchMusicByID(id);
            response = JsonSerializer.Serialize(music);
            statusCode = 200;
        }
        if (regexSYMBLE_CURRENCYMatch.Success)
        {
            var offset = int.Parse(regexMatch.Groups[1].Value);
            var limit = int.Parse(regexMatch.Groups[2].Value);
            var name = regexMatch.Groups[3].Value;
            var music = await repository.GetMusicBetterTask(offset, limit, name);
            response = JsonSerializer.Serialize(music);
            statusCode = 200;
        }
        else if (url.StartsWith("/music"))
        {
            var music = await repository.GetAllMusic();
            response = JsonSerializer.Serialize(music);
            statusCode = 200;
        }
        else
        {
            response = "Not Found";
            statusCode = 404;
        }
    }
    else if (request.HttpMethod == "POST")
    {
        var inputmusic = JsonSerializer.Deserialize<Music>(request.InputStream); 

        var music = await repository.AddMusic(inputmusic);
        response = JsonSerializer.Serialize(music);
        statusCode = 201;
    }
    else if (request.HttpMethod == "DELETE")
    {
        var regexMatch = Regex.Match(url, @"^/music/(\d+)$");
        if (regexMatch.Success)
        {
            var id = int.Parse(regexMatch.Groups[1].Value);
            await repository.RemoveMusic(id);
            response = "Deleted successful";
            statusCode = 200;
        }
        else
        {
            response = "Not Found";
            statusCode = 404;
        }
    }
    else if (request.HttpMethod == "PUT")
    {
        var inputmusic = JsonSerializer.Deserialize<Music>(request.InputStream);

        var music = await repository.UpdateMusic(inputmusic);
        if (music == null)
        {
            response = "SOME TRUBLS";
            statusCode = 400;
            return (response, statusCode);
        }
        response = JsonSerializer.Serialize(music);
        statusCode = 200;
    }
    else
    {
        response = "Method not allowed";
        statusCode = 405;
    }

    return (response, statusCode);
}
