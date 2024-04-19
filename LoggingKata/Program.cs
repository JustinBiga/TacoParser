using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            // This will display the first item in your lines array
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Use the Select LINQ method to parse every line in lines collection
            var locations = lines.Select(parser.Parse).ToArray();


            ITrackable fTacoBellA = null;   // two `ITrackable` variables with initial values of `null`.
            ITrackable fTacoBellB = null;

            double distance = 0;  // double variable to store the distance


            for (int i = 0; i < locations.Length; i++)
            {
                ITrackable locA = locations[i];
                GeoCoordinate corA = new GeoCoordinate();

                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                for (int j = 0; j < locations.Length; j++)
                {
                    ITrackable locB = locations[j];
                    GeoCoordinate corB = new GeoCoordinate();
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;

                    double distanceChecker = corA.GetDistanceTo(corB);

                    if (distanceChecker > distance)
                    {
                        distance = distanceChecker;
                        fTacoBellA = locA;
                        fTacoBellB = locB;
                    }
                }
            }
            Console.WriteLine($"The two Tacobells farthest are {fTacoBellA.Name} at coordinate {distance} and {fTacoBellB.Name} at coordinate {distance}");


        }
    }
}
