using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.Network.Entities;

namespace EoE.Network.Packets.GameEventPacket
{
    public class FinishTickPacket : IPacket<FinishTickPacket>
    {
        private bool isFinished;
        public FinishTickPacket(bool isFinished)
        {
            this.isFinished = isFinished;
        }
        public static FinishTickPacket Decode(BinaryReader reader)
        {
            return new FinishTickPacket(reader.ReadBoolean());
        }

        public static void Encode(FinishTickPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.isFinished);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                if (isFinished)
                {
                    context.PlayerSender.FinishedTick = true;
                    IServer server = (IServer)context.Receiver;
                    server.CheckPlayerTickStatus();
                }
                else
                {
                    context.PlayerSender.FinishedTick = false;
                }

            }


        }
    }
}
