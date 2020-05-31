using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpeedTest.Server.WebAPI.DB;

namespace SpeedTest.Server.WebAPI.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public ActionResult Post(object value)
        {
            Database db = new Database();
            var serialize = JsonConvert.SerializeObject(value);
            JObject jobject = JObject.Parse(serialize);
            string query = "insert into Employee (FirstName,LastName,Age) values (@FirstName,@LastName,@Age);";
            var parameters = new IDataParameter[]
            {
                new SqlParameter("@FirstName", jobject["FirstName"].ToString()),
                new SqlParameter("@LastName", jobject["LastName"].ToString()),
                new SqlParameter("@Age",jobject["Age"].ToString())
           };
            return null;
            //if (db.ExecuteData(query, parameters) > 0)
            //{

            //    return Ok(new { Result = "Saved" });
            //}
            //else
            //{
            //    return NotFound(new { Result = "something went wrong" });

            //}
        }
    }
}
