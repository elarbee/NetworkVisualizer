using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharpPcap;



namespace NetworkGraphServer
{
    class Program
    {

        public static Node[] myNodes = new Node[0];
        public static Edge[] myEdges = new Edge[0];
        public static Graph myGraph = new Graph(myNodes, myEdges);
        public static string JSONFileAddress = @"graphData.json";

        static Random r = new Random();

        //Used for checking if a ethernet frame is a broadcast
        static Byte[] BROADCAST_BYTES = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF};
        static String BROADCAST_STRING = BitConverter.ToString(BROADCAST_BYTES); 

        public static void Main(string[] args)
        {
            // Print SharpPcap version 
            string ver = SharpPcap.Version.VersionString;
            Console.WriteLine("SharpPcap {0}, Example1.IfList.cs", ver);

            // Retrieve the device list
            CaptureDeviceList devices = CaptureDeviceList.Instance;

            // If no devices were found print an error
            if (devices.Count < 1)
            {
                Console.WriteLine("No devices were found on this machine");
                return;
            }

            // Extract a device from the list
            ICaptureDevice device = devices[0];

            // Register our handler function to the
            // 'packet arrival' event
            device.OnPacketArrival +=
                new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);

            // Open the device for capturing
            int readTimeoutMilliseconds = 1000;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);

            Console.WriteLine("-- Listening on {0}, hit 'Enter' to stop...",
                device.Description);

            // Start the capturing process
            device.StartCapture();

            // Wait for 'Enter' from the user.
            Console.ReadLine();

            // Stop the capturing process
            device.StopCapture();

            // Close the pcap device
            device.Close();
        }

        private static void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            DateTime time = e.Packet.Timeval.Date;
            int len = e.Packet.Data.Length;

            Byte[] packetData = e.Packet.Data;

            //Ethernet Preamble is filtererd by the hardware so we can't capture it.
            //Byte[] preamble = new Byte[7];

            //Ethernet MAC Destination
            Byte[] destinationMAC = new byte[6];
            //Ethernet MAC Source
            Byte[] sourceMAC = new byte[6];

            //Copy Destination MAC data
            Array.Copy(packetData, 0, destinationMAC, 0, 6);
            //Copy Source MAC data
            Array.Copy(packetData, 6, sourceMAC, 0, 6);

            Console.WriteLine("Packet Recieved " + time.Second +"." + time.Millisecond);

            updateGraph(BitConverter.ToString(destinationMAC), BitConverter.ToString(sourceMAC));

            Graph g = myGraph;

            string JSON = JsonConvert.SerializeObject(g);

            System.IO.File.WriteAllText(JSONFileAddress, JSON);

        }

        private static void updateGraph(String DestinationMac, String SourceMac)
        {
            //Booleans used to check if the node already exists
            bool dNodeExists = false;
            bool sNodeExists = false;
            bool edgeExists = false;

            String destinationlabel = "MAC Address: " + DestinationMac;
            String sourcelabel = "MAC Address: " + SourceMac;

            Node dNode = new Node(DestinationMac, destinationlabel, r.NextDouble() * 10.0, r.NextDouble() * 10.0, 3);
            Node sNode = new Node(SourceMac, sourcelabel, r.NextDouble() * 10.0, r.NextDouble() * 10.0, 3);

            // NODES!!

            //Iterate through current nodes and make sure we arent adding a duplicate
            foreach (Node n in myGraph.nodes)
            {
                if(n.id == dNode.id)
                {
                    dNodeExists = true;
                }

                if(n.id == sNode.id)
                {
                    sNodeExists = true;
                }
            }

            //If the dNode is not already in our graph's array
            if (!dNodeExists)
            {
                //Create new array with an extra space for our new node.
                Node[] newNodeArray = new Node[myGraph.nodes.Length + 1];
                //Copy old array into new array
                myGraph.nodes.CopyTo(newNodeArray, 0);
                //Append new node to end
                newNodeArray[newNodeArray.Length-1] = dNode;

                myGraph.nodes = newNodeArray;
            }

            //If the dNode is not already in our graph's array
            if (!sNodeExists)
            {
                //Create new array with an extra space for our new node.
                Node[] newNodeArray = new Node[myGraph.nodes.Length + 1];
                //Copy old array into new array
                myGraph.nodes.CopyTo(newNodeArray, 0);
                //Append new node to end
                newNodeArray[newNodeArray.Length-1] = sNode;

                myGraph.nodes = newNodeArray;
            }

            // EDGES!!

            Edge newEdge = new Edge(SourceMac + "_" + DestinationMac, SourceMac, DestinationMac);

            //Iterate through current edges and make sure we arent adding a duplicate
            foreach (Edge e in myGraph.edges)
            {
                if (e.id == newEdge.id)
                {
                    edgeExists = true;
                }

            }

            //If the ethernet frame is not a broadcast, add an Edge to the graph between the destination and source ports.
            if (!DestinationMac.Equals(BROADCAST_STRING) && !DestinationMac.Equals(BROADCAST_STRING) && !edgeExists)
            {
                

                //Create new array with an extra space for our new edge.
                Edge[] newEdgeArray = new Edge[myGraph.edges.Length + 1];
                //Copy old array into new array
                myGraph.edges.CopyTo(newEdgeArray, 0);
                //Append new edge to end
                newEdgeArray[newEdgeArray.Length - 1] = newEdge;

                myGraph.edges = newEdgeArray;
            }
        }

    }

    }
