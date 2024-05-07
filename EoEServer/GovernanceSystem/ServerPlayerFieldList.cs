using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Server.GovernanceSystem
{
    public class ServerPlayerFieldList : IServerFieldList
    {
        private Dictionary<GameResourceType, int> fields = new Dictionary<GameResourceType, int>();
        private IPlayer player;
        public int TotalFieldCount
        {
            get
            {
                int count = 0;
                foreach (var kvp in fields)
                {
                    count += kvp.Value;
                }
                return count;
            }
        }

        public ServerPlayerFieldList(
            int silicon,
            int copper,
            int iron,
            int aluminum,
            int electronic,
            int industry,
            IPlayer player
            )
        {
            fields.Add(GameResourceType.Silicon, silicon);
            fields.Add(GameResourceType.Copper, copper);
            fields.Add(GameResourceType.Iron, iron);
            fields.Add(GameResourceType.Aluminum, aluminum);
            fields.Add(GameResourceType.Electronic, electronic);
            fields.Add(GameResourceType.Industrial, industry);
            this.player = player;
        }
        public void AddField(GameResourceType type, int count)
        {
            AddFieldStack(new FieldStack(type, count));
        }
        public void AddFieldStack(FieldStack adder)
        {
            fields[adder.Type] += adder.Count;
        }

        public FieldStack SplitField(GameResourceType type, int count)
        {
            int count1 = fields[type];
            if (count1 >= count)
            {
                fields[type] -= count;
                return new FieldStack(type, count);
            }
            else
            {
                fields[type] = 0;
                return new FieldStack(type, count1);
            }
        }
        public FieldStack SplitFieldStack(FieldStack field)
        {
            return SplitField(field.Type, field.Count);
        }

        public int GetFieldCount(GameResourceType type)
        {
            return fields[type];
        }

        public FieldStack GetFieldStack(GameResourceType type)
        {
            return new FieldStack(type, fields[type]);
        }

        public FieldListRecord GetFieldListRecord()
        {
            return new FieldListRecord(
                fields[GameResourceType.Silicon],
                fields[GameResourceType.Copper],
                fields[GameResourceType.Iron],
                fields[GameResourceType.Aluminum],
                fields[GameResourceType.Electronic],
                fields[GameResourceType.Industrial]

                );
        }

        public void FieldConversion(FieldStack origin, FieldStack converted)
        {
            FieldConversion(origin.Type, origin.Count, converted.Type, converted.Count);
        }
        public void FieldConversion(GameResourceType originalType, int originalcount, GameResourceType convertedType, int convertedCount)
        {
            if ((int)originalType >= (int)(GameResourceType.Aluminum))
            {
                player.SendPacket(new ServerMessagePacket("Can't convert secondary filed to primary field"));
            }
            else if ((int)convertedType <= (int)(GameResourceType.Aluminum))
            {
                player.SendPacket(new ServerMessagePacket("Can't convert one primary filed to another primary field"));
            }
            else if (originalcount != convertedCount)
            {
                player.SendPacket(new ServerMessagePacket("No enought field"));
            }
            else
            {
                SplitField(originalType, originalcount);
                AddField(convertedType, convertedCount);
                player.SendPacket(new FieldUpdatePacket(this.GetFieldListRecord()));
            }
        }
        public void ClearAll()
        {
            fields[GameResourceType.Silicon] = 0;
            fields[GameResourceType.Copper] = 0;
            fields[GameResourceType.Iron] = 0;
            fields[GameResourceType.Aluminum] = 0;
            fields[GameResourceType.Electronic] = 0;
            fields[GameResourceType.Industrial] = 0;
        }
    }

}