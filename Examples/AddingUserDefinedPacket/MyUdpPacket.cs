using PacketDotNet;
using PacketDotNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddingUserDefinedPacket
{
    public class MyUdpPacket : Packet
    {
        public MyUdpPacket()
        {
            Console.WriteLine("MyUdpPacket created.");
        }

        public MyUdpPacket(ByteArraySegment payload)
        {
            Console.WriteLine("MyUdpPacket created with parameters.");
        }

        public override String ToString(StringOutputType outputFormat)
        {
            var buffer = new StringBuilder();
            buffer.Append("[MyUdpPacket]");
            buffer.Append(base.ToString(outputFormat));
            return buffer.ToString();
        }
    }
}
