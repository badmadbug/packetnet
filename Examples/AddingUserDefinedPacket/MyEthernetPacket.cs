using PacketDotNet;
using PacketDotNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddingUserDefinedPacket
{
    public class MyEthernetPacket : Packet
    {
        public MyEthernetPacket()
        {
            Console.WriteLine("MyPacket created.");
        }

        public MyEthernetPacket(ByteArraySegment payload)
        {
            Console.WriteLine("MyPacket created with payload.");
        }

        public override String ToString(StringOutputType outputFormat)
        {
            var buffer = new StringBuilder();
            buffer.Append("[MyEthernetPacket]");
            buffer.Append(base.ToString(outputFormat));
            return buffer.ToString();
        }
    }
}
