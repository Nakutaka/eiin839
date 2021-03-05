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

        static void printError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ERR: ");
            Console.ResetColor();
            Console.WriteLine(msg);
        }

        static void printInfo(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("INFO: ");
            Console.ResetColor();
            Console.WriteLine(msg);
        }

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

        static async void getNearestStation(string contrat, GeoCoordinate targetCoordinate)
        {
            List<Station> stations = await getStationsOfContract(contrat);
            
            if (stations == null)
            {
                printError("Unable to fetch stations due to an HTTP error.");
            }
            else if(stations.Count == 0)
            {
                printInfo("No station was found.");
            }
            else
            {
                stations.Sort((station1, station2) =>
                {
                    GeoCoordinate g1 = new GeoCoordinate(station1.GetLat(), station1.GetLng());
                    GeoCoordinate g2 = new GeoCoordinate(station2.GetLat(), station2.GetLng());
                    return g1.GetDistanceTo(targetCoordinate).CompareTo(g2.GetDistanceTo(targetCoordinate));
                });

                printInfo("Target Latitude: " + targetCoordinate.Latitude + " | Target Longitude: " + targetCoordinate.Longitude);
                Console.WriteLine("*** Nearest Station ***" + stations[0]);
            }
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
            GeoCoordinate target = new GeoCoordinate(50.861784, 4.302608);

            /*            getJCDecauxContracts();
                        Console.WriteLine("\n\n");
                        getStationInfo(231, "bruxelles");*/
            getNearestStation("bruxelles", target);
            
            Console.ReadLine();
        }
    }
}