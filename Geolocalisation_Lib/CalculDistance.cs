using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocalisation_Lib
{
    public class CalculDistance
    {
        public static double getDistance(double lat1, double lon1, double lat2, double lon2)
        {
            //code for Distance in Kilo Meter
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Abs(Math.Round(rad2deg(Math.Acos(dist)) * 60 * 1.1515 * 1.609344 * 1000, 0));
            return dist / 1000;
        }
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
