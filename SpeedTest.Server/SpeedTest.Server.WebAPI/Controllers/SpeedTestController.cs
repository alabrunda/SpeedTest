using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeedTest.Data;
using SpeedTest.Domain;

namespace SpeedTest.Server.WebAPI.Controllers
{
public class SpeedResult
    {
        public string ServerName { get; set; }
        public string ClientName { get; set; }
        public int LatencyMS { get; set; }
        public int ServerID { get; set; }
        public double DistanceKM { get; set; }
        public double DownloadMbps { get; set; }
        public double UploadMbps { get; set; }
        public string IP { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SpeedTestController : ControllerBase
    {
        private readonly SpeedTestContext speedTestContext;
        public SpeedTestController(SpeedTestContext speedTestContext)
        {
            this.speedTestContext = speedTestContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //CreateCheckInEvent();
            return Ok(new { ServerVersion = "v0.2", ClinetVersion = "v0.2", Name = "speedtest"});
        }

        [HttpPost]
        public IActionResult Post(SpeedResult speedResult)
        {
            try
            {
                CreateCheckInEvent(speedResult);
                return Ok();
            }
            catch (Exception) 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            //Product deserializedProduct = JsonConvert.DeserializeObject<Product>(output);
            //CreateCheckInEvent();
        }

        private void CreateCheckInEvent(SpeedResult speedResult)
        {
            var speedTestCheckIn = new SpeedTestCheckIn {
                Download = speedResult.DownloadMbps,
                Upload = speedResult.UploadMbps,
                ServerName = speedResult.ServerName,
                Ping = speedResult.LatencyMS,
                ServerID = speedResult.ServerID,
                Distance = speedResult.DistanceKM / 1000,
                TestDate = DateTime.UtcNow,
                ClientIP = speedResult.IP ?? this.HttpContext.Connection.RemoteIpAddress.ToString(),//Docker messes up IP
                ClientName = speedResult.ClientName
            };
            speedTestContext.SpeedTestCheckIns.Add(speedTestCheckIn);
            speedTestContext.SaveChanges();
        }
    }
}