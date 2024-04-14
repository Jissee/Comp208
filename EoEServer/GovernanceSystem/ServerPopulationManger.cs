using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public class ServerPopulationManger: IServerPopManager
    {
        private Dictionary<GameResourceType, int> popAloc = new Dictionary<GameResourceType, int>();
        private IPlayer player;

        public int ExploratoinPopulation { get; private set; }
        public int AvailablePopulation { get; private set; }

        public int TotalPopulation
        {
            get 
            {
                int count = 0;
                foreach (var kvp in popAloc)
                {
                    count += kvp.Value;
                }

                count += AvailablePopulation;
                return count;
            }
        }

        public ServerPopulationManger(int initPop, IPlayer player)
        {
            popAloc.Add(GameResourceType.Silicon, 0);
            popAloc.Add(GameResourceType.Copper, 0);
            popAloc.Add(GameResourceType.Iron, 0);
            popAloc.Add(GameResourceType.Aluminum, 0);
            popAloc.Add(GameResourceType.Industrial, 0);
            popAloc.Add(GameResourceType.Electronic, 0);
            AvailablePopulation = initPop;
            this.player = player;
        }

        /// <summary>
        /// Set population allocation, send serverMessage packet if invalid allocation
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <exception cref="InvalidPopAllocException"></exception>
        public void SetAllocation(int siliconPop, int copperPop, int ironPop, int aluminumPop, int industrialPop,int electronicPop)
        {
            if (CheckAvailability(siliconPop, copperPop, ironPop, aluminumPop, industrialPop, electronicPop))
            {
                AvailablePopulation = TotalPopulation - siliconPop - copperPop - ironPop - aluminumPop - industrialPop - electronicPop;
                popAloc[GameResourceType.Silicon] = siliconPop;
                popAloc[GameResourceType.Copper] = copperPop;
                popAloc[GameResourceType.Iron] = ironPop;
                popAloc[GameResourceType.Aluminum] = aluminumPop;
                popAloc[GameResourceType.Industrial] = industrialPop;
                popAloc[GameResourceType.Electronic] = electronicPop;
            }
            else
            {
                player.SendPacket(new PopulationUpdatePacket(GetPopulationRecord()));
            }
        }

        private bool CheckAvailability(int siliconPop, int copperPop, int ironPop, int aluminumPop, int industrialPop, int electronic)
        {
            List<int> list = [siliconPop, copperPop, ironPop, aluminumPop, industrialPop, electronic];
            if (list.Min() < 0)
            {
                player.SendPacket(new ServerMessagePacket("Negative input"));
                return false;
            }else if (siliconPop + copperPop + ironPop + aluminumPop + industrialPop + electronic + AvailablePopulation >= TotalPopulation)
            {
                return true;
            }
            else
            {
                player.SendPacket(new ServerMessagePacket("Invalid population allocation"));
                return false;
            }

        }
        public void AlterPop(int count)
        {
            AvailablePopulation += count;
            if (AvailablePopulation < 0)
            {
                if (TotalPopulation < 0)
                {
                    return;
                }

                int decreasing = -AvailablePopulation;

                for (int i = 0; i < decreasing; i++)
                {
                    GameResourceType type = (GameResourceType)GetNextIndex();
                    if (popAloc[type] > 0)
                    {
                        popAloc[type] --;
                    }
                    else
                    {
                        i--;
                    }
                }
                AvailablePopulation = 0;
            }
        }

        private int index = -1;
        private int GetNextIndex()
        {
            index++;
            return (index %= 6);
        }

        public int GetPopAllocCount(GameResourceType type)
        {
            return popAloc[type];
        }

        public void SetExploration(int population)
        {
            AlterPop(-population);
            ExploratoinPopulation = population;

        }

        public PopulationRecord GetPopulationRecord()
        {
            return new PopulationRecord(
                popAloc[GameResourceType.Silicon],
                popAloc[GameResourceType.Copper],
                popAloc[GameResourceType.Iron],
                popAloc[GameResourceType.Aluminum],
                popAloc[GameResourceType.Electronic],
                popAloc[GameResourceType.Industrial],      
                AvailablePopulation
                );
        }

        public void ClearAll()
        {
            foreach (var key in popAloc.Keys.ToList())
            {
                popAloc[key] = 0;
                AvailablePopulation = 0;
            }
        }
    }
}
