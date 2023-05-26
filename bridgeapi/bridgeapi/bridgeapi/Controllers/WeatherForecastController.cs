using Microsoft.AspNetCore.Mvc;

namespace bridgeapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
            
        }

        //THIS IS THE VERSION I WANT TO DEBUG WITH BRIDGE
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> Get()
        {
            //dependency on K8s
            var dependencyResponse = await _client.
                CreateClient().
                GetAsync("http://dependencyapisvc.default.svc.cluster.local/weatherforecast");
            return await dependencyResponse.Content.ReadAsStringAsync();
           
        }

        /*[HttpGet(Name = "GetWeatherForecast")] ==> THIS IS THE VERSION RUNNING CURRENTLY IN THE CLUSTER
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
    }
}