using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Server.GovernanceSystem
{
    public class ServerPlayerPopManger : IServerPopManager
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

        public ServerPlayerPopManger(int initPop, IPlayer player)
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
        public void SetAllocation(PopulationRecord record)
        {
            if (CheckAvailability(record))
            {
                int total = TotalPopulation;
                AvailablePopulation = total - record.siliconPop - record.copperPop - record.ironPop - record.aluminumPop - record.industrialPop - record.electronicPop;
                popAloc[GameResourceType.Silicon] = record.siliconPop;
                popAloc[GameResourceType.Copper] = record.copperPop;
                popAloc[GameResourceType.Iron] = record.ironPop;
                popAloc[GameResourceType.Aluminum] = record.aluminumPop;
                popAloc[GameResourceType.Industrial] = record.industrialPop;
                popAloc[GameResourceType.Electronic] = record.electronicPop;
                player.SendPacket(new PopulationUpdatePacket(GetPopulationRecord()));
            }
            else
            {
                player.SendPacket(new ServerMessagePacket("Invalid population allocation"));
            }
        }

        private bool CheckAvailability(PopulationRecord record)
        {
            List<int> list = [
                record.siliconPop,
                record.copperPop,
                record.ironPop,
                record.aluminumPop,
                record.industrialPop,
                record.electronicPop
                ];
            if (list.Min() < 0)
            {
                player.SendPacket(new ServerMessagePacket("Negative input"));
                return false;
            }
            else if (TotalPopulation >=
                record.siliconPop +
                record.copperPop +
                record.ironPop +
                record.aluminumPop +
                record.industrialPop +
                record.electronicPop
                )
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
                        popAloc[type]--;
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
