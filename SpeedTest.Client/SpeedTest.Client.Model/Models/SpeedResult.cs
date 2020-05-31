using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedTest.Client.Model.Models
{
    public class SpeedResult
    {
        public string ServerName { get; set; }
        public string ClientName { get; set; }
        public int LatencyMS { get; set; }
        public int ServerID{ get; set; }
        public double DistanceKM { get; set; }
        public double DownloadMbps { get; set; }
        public double UploadMbps { get; set; }
        public string IP { get; set; }
            }
}
