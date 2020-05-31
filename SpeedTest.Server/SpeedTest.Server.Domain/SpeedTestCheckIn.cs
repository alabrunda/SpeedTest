using System;
using System.ComponentModel.DataAnnotations;

namespace SpeedTest.Domain
{
    public class SpeedTestCheckIn
    {
        public SpeedTestCheckIn()
        {

        }
        [Key]
        public int ID { get; set; }
        public double Download { get; set; }
        public double Upload { get; set; }
        public string ClientIP { get; set; }
        public int ServerID { get; set; }
        public double Distance { get; set; }
        public int Ping { get; set; }
        public string ClientName { get; set; }

        [MaxLength(40)]
        public string ServerName { get; set; }
        public DateTime TestDate { get; set; }

    }
}
