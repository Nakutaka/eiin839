using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace TD3
{
    class Station
    {
        public string contract_name { get; set; }
        public int number { get; set; }

        public Position position { get; set; }

        public Station() { }

        public Station(string name, int number, Position position)
        {
            this.contract_name = name;
            this.number = number;
            this.position = position;
        }

        public double getLat()
        {
            return position.lat;
        }

        public double getLng()
        {
            return position.lng;
        }

        override
        public string ToString()
        {
            string str = "\nContract_Name: " + contract_name + "\nNumber: " + number + "\n" + position.ToString();
            return str;
        }
    }
}
