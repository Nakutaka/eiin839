using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD3
{
    class Position
    {
        public double lat {get; set;}
        public double lng { get; set;}

        public Position(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }

        override
        public string ToString()
        {
            string str = "Lat: " + lat + "\nLng: " + lng;
            return str;
        }
    }
}
