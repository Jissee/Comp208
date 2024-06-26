﻿using EoE.Governance;
using EoE.Governance.ClientInterface;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Client.Governance
{
    public class ClientFieldList : IClientFieldList
    {
        private Dictionary<GameResourceType, int> fields = new Dictionary<GameResourceType, int>();
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

        public ClientFieldList(
            int silicon,
            int copper,
            int iron,
            int aluminum,
            int electronic,
            int industry
            )
        {
            fields.Add(GameResourceType.Silicon, silicon);
            fields.Add(GameResourceType.Copper, copper);
            fields.Add(GameResourceType.Iron, iron);
            fields.Add(GameResourceType.Aluminum, aluminum);
            fields.Add(GameResourceType.Electronic, electronic);
            fields.Add(GameResourceType.Industrial, industry);
        }

        public ClientFieldList() : this(20, 20, 20, 20, 20, 20)
        {

        }

        public List<int> ToList()
        {
            List<int> list =
                [this.GetFieldCount(GameResourceType.Silicon),
                this.GetFieldCount(GameResourceType.Copper),
                this.GetFieldCount(GameResourceType.Iron),
                this.GetFieldCount(GameResourceType.Aluminum),
                this.GetFieldCount(GameResourceType.Electronic),
                this.GetFieldCount(GameResourceType.Industrial)];
            return list;
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

        public void Synchronize(FieldListRecord fieldListRecord)
        {
            fields[GameResourceType.Silicon] = fieldListRecord.siliconFieldCount;
            fields[GameResourceType.Copper] = fieldListRecord.copperFieldCount;
            fields[GameResourceType.Iron] = fieldListRecord.ironFieldCount;
            fields[GameResourceType.Aluminum] = fieldListRecord.aluminumFieldCount;
            fields[GameResourceType.Electronic] = fieldListRecord.electronicFieldCount;
            fields[GameResourceType.Industrial] = fieldListRecord.industrialFieldCount;
            WindowManager.INSTANCE.UpdateFields();
        }
        public void FieldConversion(FieldStack origin, FieldStack converted)
        {
            FieldConversion(origin.Type, origin.Count, converted.Type, converted.Count);
        }
        public void FieldConversion(GameResourceType originalType, int originalcount, GameResourceType convertedType, int convertedCount)
        {
            if ((int)originalType >= (int)(GameResourceType.Aluminum))
            {
                Client.INSTANCE.MsgBox("Can't convert secondary filed to primary field");
            }
            else if ((int)convertedType <= (int)(GameResourceType.Aluminum))
            {
                Client.INSTANCE.MsgBox("Can't convert one primary filed to another primary field");
            }
            else if (originalcount != convertedCount)
            {
                Client.INSTANCE.MsgBox("No enought field");
            }
            else
            {
                Client.INSTANCE.SendPacket(new FieldConvertPacket(new FieldStack(originalType, originalcount), new FieldStack(convertedType, convertedCount)));
            }
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
    }
}
