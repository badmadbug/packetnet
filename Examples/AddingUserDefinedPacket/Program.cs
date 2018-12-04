using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketDotNet;

namespace AddingUserDefinedPacket
{
    class Program
    {
        static void Main(string[] args)
        {
            RunOldExample();

            RunNewEthernetPacket();
            RunNewUdpPacket();
        }

        private static void RunNewUdpPacket()
        {
            UdpPacket.SubPacketBySrc[123] = typeof(MyUdpPacket);

            byte[] payload = CreatePacket();

            Packet packet = Packet.ParsePacket(LinkLayers.Ethernet, payload);
            Console.WriteLine(packet.ToString());
        }

        static void RunNewEthernetPacket()
        {
            EthernetPacket.SubPacketTypes[0x1234] = typeof(MyEthernetPacket);

            byte[] payload = CreatePacket(0x1234);
            Packet packet = Packet.ParsePacket(LinkLayers.Ethernet, payload);

            Console.WriteLine(packet.ToString());
        }

        static void RunOldExample()
        { 
            byte[] payload = CreatePacket();

            Packet packet = Packet.ParsePacket(LinkLayers.Ethernet, payload);
            Console.WriteLine(packet.ToString());
        }

        static byte[] CreatePacket(int packetType = (int)EthernetPacketType.IPv4, int srcPort = 123, int dstPort = 321)
        {
            UInt16 tcpSourcePort = 123;
            UInt16 tcpDestinationPort = 321;
            var udpPacket = new UdpPacket(tcpSourcePort, tcpDestinationPort);

            var ipSourceAddress = System.Net.IPAddress.Parse("192.168.1.1");
            var ipDestinationAddress = System.Net.IPAddress.Parse("192.168.1.2");
            var ipPacket = new IPv4Packet(ipSourceAddress, ipDestinationAddress);

            var sourceHwAddress = "90-90-90-90-90-90";
            var ethernetSourceHwAddress = System.Net.NetworkInformation.PhysicalAddress.Parse(sourceHwAddress);
            var destinationHwAddress = "80-80-80-80-80-80";
            var ethernetDestinationHwAddress = System.Net.NetworkInformation.PhysicalAddress.Parse(destinationHwAddress);
            // NOTE: using EthernetPacketType.None to illustrate that the ethernet
            //       protocol type is updated based on the packet payload that is
            //       assigned to that particular ethernet packet
            var ethernetPacket = new EthernetPacket(ethernetSourceHwAddress,
                                                    ethernetDestinationHwAddress,
                                                    EthernetPacketType.None);

            // Now stitch all of the packets together
            ipPacket.PayloadPacket = udpPacket;
            ethernetPacket.PayloadPacket = ipPacket;

            // and print out the packet to see that it looks just like we wanted it to
            byte[] payload = ethernetPacket.Bytes;

            // it can be crc error. but this is just test.
            payload[12] = (byte)(packetType / 0x0100);
            payload[13] = (byte)(packetType % 0x0100);
            return payload;
        }
    }
}
