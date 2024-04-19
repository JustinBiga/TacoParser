using System;

namespace LoggingKata
{
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            //line splitted up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            // If your array's Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                // Log error message and return null
                return null; 
            }

            var latitude = double.Parse(cells[0]);

            var longitude = double.Parse(cells[1]);

            string locationName = cells[2];     // name as a string

            Point point = new Point();
            point.Longitude = longitude;
            point.Latitude = latitude;

            TacoBell tacoBell = new TacoBell(locationName, point);
            tacoBell.Name = locationName;
            tacoBell.Location = point;

            // TODO: Then, return the instance of your TacoBell class,
            // since it conforms to ITrackable

            return tacoBell;
        }

       
    }
}
