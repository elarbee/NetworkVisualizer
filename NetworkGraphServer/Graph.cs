using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkGraphServer
{

    class Graph
    {
        public Node[] nodes;
        public Edge[] edges;

        public Graph(Node[] Nodes, Edge[] Edges)
        {
            nodes = Nodes;
            edges = Edges;
        }
    }
}
