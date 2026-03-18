using System.Text.Json;

using WebApiLab.Console.Models;

HttpClient client = new HttpClient();

client.BaseAddress = new Uri("http://localhost:5029");

HttpResponseMessage response = await client.GetAsync("/api/People/V59OF92YF627HFY0)");
if (response.IsSuccessStatusCode)
{
    string json = await response.Content.ReadAsStringAsync();
    var persons = JsonSerializer.Deserialize<List<Person>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    foreach (var person in persons)
    {
        Console.WriteLine($"{person.Name} speaks {person.Language}");
    }
}
else
{
    Console.WriteLine($"Error: {response.StatusCode}");
    Console.WriteLine(await response.Content.ReadAsStringAsync());
}