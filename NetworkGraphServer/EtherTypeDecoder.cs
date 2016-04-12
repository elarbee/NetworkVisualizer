using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkGraphServer
{
    class EtherTypeDecoder
    {
        public string decode(byte[] b)
        {
            if (b.Length>2)
            {
                throw new System.ArgumentException("EtherType must be byte array of lenght 2!", "original");
            }
            //Convert byte array into string which is easier to compare
            string byteString = BitConverter.ToString(b).Replace("-", string.Empty);

            switch (byteString)
            {
                case "0800":
                    return "IPv4";
                case "0806":
                    return "ARP";
                case "0842":
                    return "Wake-on-LAN";
                case "22F3":
                    return "IETF TRILL Protocol";
                case "6003":
                    return "DECnet Phase IV";
                case "8035":
                    return "RARP";
                case "809B":
                    return "AppleTalk";
                case "80F3":
                    return "AppleTalk ARP";
                case "8100":
                    return "VLAN-tagged frame  and Shortest Path Bridging IEEE 802.1aq";
                case "8137":
                    return "IPX";
                case "8204":
                    return "QNX Qnet";
                case "86DD":
                    return "IPv6";
                case "8808":
                    return "Ethernet flow control";
                case "8819":
                    return "CobraNet";
                case "8847":
                    return "MPLS unicast";
                case "8848":
                    return "MPLS multicast";
                case "8863":
                    return "PPPoE Discovery Stage";
                case "8864":
                    return "PPPoE Session Stage";
                case "8870":
                    return "Jumbo Frames";
                case "887B":
                    return "HomePlug 1.0 MME";
                case "888E":
                    return "EAP over LAN";
                case "8892":
                    return "PROFINET Protocol";
                case "889A":
                    return "HyperSCSI";
                case "88A2":
                    return "ATA over Ethernet";
                case "88A4":
                    return "EtherCAT Protocol";
                case "88A8":
                    return "Provider Bridging  & Shortest Path Bridging IEEE 802.1aq";
                case "88AB":
                    return "Ethernet Powerlink";
                case "88CC":
                    return "Link Layer Discovery Protocol";
                case "88CD":
                    return "SERCOS III";
                case "88E1":
                    return "HomePlug AV MME";
                case "88E3":
                    return "Media Redundancy Protocol";
                case "88E5":
                    return "MAC security";
                case "88E7":
                    return "Provider Backbone Bridges";
                case "88F7":
                    return "Precision Time Protocol";
                case "8902":
                    return "IEEE 802.1ag Connectivity Fault Management";
                case "8906":
                    return "Fibre Channel over Ethernet";
                case "8914":
                    return "FCoE Initialization Protocol";
                case "8915":
                    return "RDMA over Converged Ethernet";
                case "891D":
                    return "TTEthernet Protocol Control Frame";
                case "892F":
                    return "High-availability Seamless Redundancy";
                case "9000":
                    return "Ethernet Configuration Testing Protocol";
            }

            return "Invalid EtherType";
        }
    }


}
