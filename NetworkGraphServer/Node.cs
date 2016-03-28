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
        public int x;
        public int y;
        public int size;

        public Node(string ID,string Label,int X,int Y, int Size)
        {
            id = ID;
            label = Label;
            x = X;
            y = Y;
            size = Size;
        }
    }
}
