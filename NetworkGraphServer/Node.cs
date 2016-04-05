using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkGraphServer
{
    class Node
    {
        public string id;
        public string label;
        public double x;
        public double y;
        public int size;

        public Node(string ID,string Label,double X,double Y, int Size)
        {
            id = ID;
            label = Label;
            x = X;
            y = Y;
            size = Size;
        }
    }
}
