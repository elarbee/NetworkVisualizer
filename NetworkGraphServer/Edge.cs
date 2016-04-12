using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkGraphServer
{
    class Edge
    {
        public string id;
        public string label;
        public string source;
        public string target;


        public Edge(string ID,string Label, string Source, string Target)
        {
            id = ID;
            label = Label;
            source = Source;
            target = Target;
        }
    }
}
