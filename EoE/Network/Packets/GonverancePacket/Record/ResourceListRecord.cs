﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;

namespace EoE.Network.Packets.GonverancePacket.Record
{
    public struct ResourceListRecord
    {
        public int siliconCount;
        public int copperCount;
        public int ironCount;
        public int aluminumCount;
        public int electronicCount;
        public int industrialCount;
        public int battleArmyCount;
        public int informativeArmyCount;
        public int mechanismArmyCount;

        public ResourceListRecord(IResourceList resourceList)
        {
            siliconCount = resourceList.GetResourceCount(GameResourceType.Silicon);
            copperCount = resourceList.GetResourceCount(GameResourceType.Copper);
            ironCount = resourceList.GetResourceCount(GameResourceType.Iron);
            aluminumCount = resourceList.GetResourceCount(GameResourceType.Aluminum);
            electronicCount = resourceList.GetResourceCount(GameResourceType.Electronic);
            industrialCount = resourceList.GetResourceCount(GameResourceType.Industrial);
            battleArmyCount = resourceList.GetResourceCount(GameResourceType.BattleArmy);
            informativeArmyCount = resourceList.GetResourceCount(GameResourceType.InformativeArmy);
            mechanismArmyCount = resourceList.GetResourceCount(GameResourceType.MechanismArmy);

        }
        public ResourceListRecord
            (
            int silicon,
            int coppor,
            int iron,
            int aluminum,
            int electronic,
            int industrial,
            int battleArmy,
            int informativeArmy,
            int mechanismArmy
            )
        {
            siliconCount = silicon;
            copperCount = coppor;
            ironCount = iron;
            aluminumCount = aluminum;
            electronicCount = electronic;
            industrialCount = industrial;
            battleArmyCount = battleArmy;
            informativeArmyCount = informativeArmy;
            mechanismArmyCount = mechanismArmy;

        }
        public ResourceListRecord() : this(0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        public static Encoder<ResourceListRecord> encoder = (ResourceListRecord obj, BinaryWriter writer) =>
        {
            writer.Write(obj.siliconCount);
            writer.Write(obj.copperCount);
            writer.Write(obj.ironCount);
            writer.Write(obj.aluminumCount);
            writer.Write(obj.electronicCount);
            writer.Write(obj.industrialCount);
            writer.Write(obj.battleArmyCount);
            writer.Write(obj.informativeArmyCount);
            writer.Write(obj.mechanismArmyCount);
        };

        public static Decoder<ResourceListRecord> decoder = (BinaryReader reader) =>
        {
            return new ResourceListRecord(
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count
                );
        };
    }
    
}
