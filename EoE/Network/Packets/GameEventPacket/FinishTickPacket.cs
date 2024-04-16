using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Entities;

namespace EoE.Network.Packets.GameEventPacket
{
    public class FinishTickPacket : IPacket<FinishTickPacket>
    {
        private bool isFinished;
        private int tickCount;
        public FinishTickPacket(bool isFinished, int tickCount)
        {
            this.isFinished = isFinished;
            this.tickCount = tickCount;
        }
        public static FinishTickPacket Decode(BinaryReader reader)
        {
            return new FinishTickPacket(reader.ReadBoolean(),reader.ReadInt32());
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
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    if (isFinished)
                    {
                        client.SynchronizeTickCount(tickCount);
                    }
                }
            }

        }
    }
}
