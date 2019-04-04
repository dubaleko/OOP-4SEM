using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
   public  class Adress
    {
        public string city { get; set; }
        public string index { get; set; }
        public string  street { get; set; }
        public string house { get; set; }
        public string  flat { get; set; }

        public Adress(string city, string index, string street, string house, string flat)
        {
            this.city = city;
            this.index = index;
            this.street = street;
            this.house = house;
            this.flat = flat;
        }
        public Adress()
        {

        }
    }
}
