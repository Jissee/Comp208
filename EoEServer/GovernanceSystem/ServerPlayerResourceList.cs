using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EoE.Server.GovernanceSystem
{
    public class ServerPlayerResourceList:IResourceList
    {
        public ResourceStack CountrySilicon { get; init; }
        public ResourceStack CountryCopper {get;init;}
        public ResourceStack CountryIron {get;init;}
        public ResourceStack CountryAluminum {get;init;}
        public ResourceStack CountryElectronic {get;init;}
        public ResourceStack CountryIndustrial {get;init;}

        public ResourceStack CountryBattleArmy { get; init; }
        public ResourceStack CountryInformativeArmy { get; init; }
        public ResourceStack CountryMechanismArmy { get; init; }
        public ServerPlayerResourceList(
            int silicon,
            int copper,
            int iron, 
            int aluminum,
            int electronic, 
            int industrial, 
            int battleArmy, 
            int informativeArmy, 
            int mechanismArmy)
        {
            CountrySilicon = new ResourceStack(GameResourceType.Silicon, silicon);
            CountryCopper = new ResourceStack(GameResourceType.Copper, copper);
            CountryIron = new ResourceStack(GameResourceType.Iron, iron);
            CountryAluminum = new ResourceStack(GameResourceType.Aluminum, aluminum);
            CountryElectronic = new ResourceStack(GameResourceType.Electronic, electronic);
            CountryIndustrial = new ResourceStack(GameResourceType.Industrial, industrial);
            CountryBattleArmy = new ResourceStack(GameResourceType.BattleArmy, battleArmy);
            CountryInformativeArmy = new ResourceStack(GameResourceType.InformativeArmy, informativeArmy);
            CountryMechanismArmy = new ResourceStack(GameResourceType.MechanismArmy, mechanismArmy);
        }
        public ServerPlayerResourceList() : this(0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        public void AddResource(ResourceStack adder)
        {
            GameResourceType type = adder.Type;
            switch (type)
            {
                case GameResourceType.Silicon:
                    CountrySilicon.Add(adder);
                    break;
                case GameResourceType.Copper:
                    CountryCopper.Add(adder);
                    break;
                case GameResourceType.Iron:
                    CountryIron.Add(adder);
                    break;
                case GameResourceType.Aluminum:
                    CountryAluminum.Add(adder);
                    break;
                case GameResourceType.Electronic:
                    CountryElectronic.Add(adder);
                    break;
                case GameResourceType.Industrial:
                    CountryIndustrial.Add(adder);
                    break;
                case GameResourceType.BattleArmy:
                    CountryBattleArmy.Add(adder);
                    break;
                case GameResourceType.InformativeArmy:
                    CountryInformativeArmy.Add(adder);
                    break;
                case GameResourceType.MechanismArmy:
                    CountryMechanismArmy.Add(adder);
                    break;
                default:
                    throw new Exception("no such type");
            }
        }
        public ResourceStack SplitResourceStack(GameResourceType type, int count)
        {
            return SplitResourceStack(new ResourceStack(type, count));
        }
        public ResourceStack SplitResourceStack(ResourceStack stack)
        {
            GameResourceType type = stack.Type;
            switch (type)
            {
                case GameResourceType.Silicon:
                    return CountrySilicon.Split(stack.Count);
                case GameResourceType.Copper:
                    return CountryCopper.Split(stack.Count);
                case GameResourceType.Iron:
                    return CountryIron.Split(stack.Count);
                case GameResourceType.Aluminum:
                    return CountryAluminum.Split(stack.Count);
                case GameResourceType.Electronic:
                    return CountryElectronic.Split(stack.Count);
                case GameResourceType.Industrial:
                    return CountryIndustrial.Split(stack.Count);
                case GameResourceType.BattleArmy:
                    return CountryBattleArmy.Split(stack.Count);
                case GameResourceType.InformativeArmy:
                    return CountryInformativeArmy.Split(stack.Count);
                case GameResourceType.MechanismArmy:
                    return CountryMechanismArmy.Split(stack.Count);
                default:
                    throw new Exception("no such type");
            }
        }
        public int GetResourceCount(GameResourceType type)
        {
            return ((IResourceList)this).GetResourceCount(type);
        }
    }
}
