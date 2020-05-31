using System.Xml.Serialization;

namespace SpeedTest.Client.Model.Models
{
    [XmlRoot("server-config")]
    public class ServerConfig
    {
        [XmlAttribute("ignoreids")]
        public string IgnoreIds { get; set; }
    }
}