using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Device.Location;

namespace TD3
{
     
    class Program
    {
        private static string API_KEY = "21cd0d074e546da8f6a413eaad3243855368e4be";
        static readonly HttpClient client = new HttpClient();

        static async void getJCDecauxContracts()
        {
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/contracts?apiKey=" + API_KEY);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Contracts : ");
                Console.ResetColor();
                Console.WriteLine(responseBody);

            }
            catch (HttpRequestException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Http error : ");
                Console.WriteLine(ex);
                Console.ResetColor();
            }

        }

        static async Task<List<Station>> getStationsOfContract(string contract)
        {
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + "&apiKey=" + API_KEY);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<Station> stations = JsonSerializer.Deserialize<List<Station>>(responseBody);
                return stations;

            }
            catch (HttpRequestException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Http error : ");
                Console.WriteLine(ex);
                Console.ResetColor();
            }
            return null;
        }
        static async void getStationInfo(int stationId, string contract)
        {
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync("https://api.jcdecaux.com/vls/v3/stations/" + stationId + "?contract=" + contract + "&apiKey=" + API_KEY);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Infos of station ID " + stationId + " and contract named " + contract + " :");
                Console.ResetColor();
                Console.WriteLine(responseBody);

            }
            catch (HttpRequestException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Http error : ");
                Console.WriteLine(ex);
                Console.ResetColor();
            }
        }

        static async Task<Station> getNearestStation(string contrat, GeoCoordinate targetCoordinate)
        {
            List<Station> stations = await getStationsOfContract(contrat);
            stations.Sort((station1, station2) =>
            {
                GeoCoordinate g1 = new GeoCoordinate(station1.getLat(), station1.getLng());
                GeoCoordinate g2 = new GeoCoordinate(station2.getLat(), station2.getLng());
                return g1.GetDistanceTo(targetCoordinate).CompareTo(g2.GetDistanceTo(targetCoordinate));
            });

            return stations[0];
        }

        static async void printStationsOfContract(string contract)
        {
            List<Station> stations = await getStationsOfContract(contract);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Stations of contract named " + contract + " :");
            Console.ResetColor();
            stations.ForEach(Console.WriteLine);
        }

        static void Main(string[] args)
        {
            /*            getJCDecauxContracts();
                        Console.WriteLine("\n\n");
                        getStationInfo(231, "bruxelles");*/
            //Station station = await getNearestStation("bruxelles")
            MainAsync().GetAwaiter().GetResult();
            

            Console.ReadLine();
        }

        private static async Task MainAsync()
        {
            //printStationsOfContract("bruxelles");
            GeoCoordinate target = new GeoCoordinate(50.861784, 4.302608);
            Station nearest = await getNearestStation("bruxelles", target);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Target Latitude: " + target.Latitude + "\nTarget Longitude: " + target.Longitude);
            Console.WriteLine("*** Nearest Station ***" + nearest);
            Console.ResetColor();
        }
    }
}
