using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeedTest.Client.Model;
using SpeedTest.Client.Model.Models;

namespace SpeedTest.Client.Console
{
    class Program
    {
        private static SpeedTestClient client;
        private static Settings settings;

        static async Task Main()
        {            
            string speedTestServerURL = Environment.GetEnvironmentVariable("SpeedTestServerURL") ?? "http://localhost:8080/api/speedtest";
            string clientName = Environment.GetEnvironmentVariable("ClientName") ?? "Env:ClientName";
            System.Console.WriteLine("v0.02 Client: " + clientName +  " Target: " + speedTestServerURL);

            string publicIPv4 = "NoIP";
            bool serverAccessible = false;
            try
            {
                publicIPv4 = await SpeedTestClient.GetPublicIPv4AddressAsync();
                await SpeedTestClient.ValidateAPIAccess(speedTestServerURL);
                serverAccessible = true;
                System.Console.WriteLine("Success - PublicIP: " + publicIPv4 + " Accessible: " + speedTestServerURL);
            }
            catch (Exception) 
            {
                System.Console.WriteLine("Exception - PublicIP: " + publicIPv4 + " Accessible: " + serverAccessible);
                Environment.Exit(0);
            }

            client = new SpeedTestClient();
            settings = client.GetSettings();

            //var servers = SelectServers();
            var bestServer = SelectDesiredServer(settings.Servers);

            foreach (var server in bestServer)
            {
                SpeedResult speedResult = new SpeedResult();
                speedResult.ServerName = server.Sponsor;
                speedResult.LatencyMS = server.Latency;
                speedResult.ServerID = server.Id;
                speedResult.IP = publicIPv4;
                speedResult.ClientName = clientName;
                speedResult.DistanceKM = Math.Round(server.Distance);
                speedResult.DownloadMbps = Math.Round(client.TestDownloadSpeed(server, settings.Download.ThreadsPerUrl) / 1024, 2);
                speedResult.UploadMbps = Math.Round(client.TestUploadSpeed(server, settings.Upload.ThreadsPerUrl) / 1024, 2);
                await SpeedTestClient.SendSpeedResult(speedResult, speedTestServerURL);
            }
        }

        private static IEnumerable<Server> SelectDesiredServer(IEnumerable<Server> servers)
        {
            var bestServer = from svr in servers
                             where svr.Id == 3803 || svr.Id == 30964
                             select svr;

            foreach (var server in bestServer)
            {
                server.Latency = client.TestServerLatency(server);
                //PrintServerDetails(server);
            }

            return bestServer;
        }

        private static Server SelectBestServer(IEnumerable<Server> servers)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Best server by latency:");
            var bestServer = servers.OrderBy(x => x.Latency).First();
            PrintServerDetails(bestServer);
            System.Console.WriteLine();
            return bestServer;
        }

        private static IEnumerable<Server> SelectServers()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Selecting best server by distance...");
            var servers = settings.Servers.Take(10).ToList();

            foreach (var server in servers)
            {
                server.Latency = client.TestServerLatency(server);
                PrintServerDetails(server);
            }
            return servers;
        }

        private static void PrintServerDetails(Server server)
        {
            System.Console.WriteLine("Hosted by {0} ({1}/{2}), distance: {3}km, latency: {4}ms", server.Sponsor, server.Name,
                server.Country, (int)server.Distance / 1000, server.Latency);
        }

        private static void PrintSpeed(string type, double speed)
        {
            if (speed > 1024)
            {
                System.Console.WriteLine("{0} speed: {1} Mbps", type, Math.Round(speed / 1024, 2));
            }
            else
            {
                System.Console.WriteLine("{0} speed: {1} Kbps", type, Math.Round(speed, 2));
            }
        }
    }
}
