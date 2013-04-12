using FuelTracker_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService1.Outil
{
    [DataContract]
    public class StationAndDistance : IComparable
    {
        [DataMember]
        public Station station;
        [DataMember]
        public double distanceStation;

        public StationAndDistance(Station station, double distanceStation)
        {
            this.station = station;
            this.distanceStation = distanceStation;
        }

        public void setPrice(List<Prix> price_list)
        {
            station.setPrice(price_list);
        }

        public string getIdStation()
        {
            return station.getIdStation();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            StationAndDistance otherStation = obj as StationAndDistance;
            if (otherStation != null)
                return this.distanceStation.CompareTo(otherStation.distanceStation);
            else
                throw new ArgumentException("Object is not a Station");
        }
    }
}