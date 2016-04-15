using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkGraphServer
{
    class Frame
    {
        public string destination;
        public string source;
        public string ethertype;
        public string data;

        public Frame(string Destination, string Source, string Ethertype, string Data)
        {
            destination = Destination;
            source = Source;
            ethertype = Ethertype;
            data = Data;
        }
    }
}
