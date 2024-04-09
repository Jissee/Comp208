using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public class ServerPopulationManger: IServerPopManager
    {
        public int SiliconPop { get; private set; }
        public int CopperPop { get; private set; }
        public int IronPop { get; private set; }
        public int AluminumPop { get; private set; }
        public int ElectronicPop { get; private set; }
        public int IndustrailPop { get; private set; }

        public int AvailablePopulation { get; private set; }

        public int TotalPopulation => SiliconPop + CopperPop +
            IronPop + AluminumPop + ElectronicPop +
            IndustrailPop + AvailablePopulation;

        public ServerPopulationManger()
        {
            SiliconPop = 0;
            CopperPop = 0;
            IronPop = 0;
            AluminumPop = 0;
            ElectronicPop = 0;
            IndustrailPop = 0;
            AvailablePopulation = 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <exception cref="InvalidPopAllocException"></exception>
        public void SetAllocation(GameResourceType type, int count)
        {
            switch (type)
            {
                case GameResourceType.Silicon:
                    SiliconPop = TrySet(SiliconPop, count);
                    break;
                case GameResourceType.Copper:
                    CopperPop = TrySet(CopperPop, count);
                    break;
                case GameResourceType.Iron:
                    IronPop = TrySet(IronPop, count);
                    break;
                case GameResourceType.Aluminum:
                    AluminumPop = TrySet(AluminumPop, count);
                    break;
                case GameResourceType.Electronic:
                    ElectronicPop = TrySet(ElectronicPop, count);
                    break;
                case GameResourceType.Industrial:
                    IndustrailPop = TrySet(IndustrailPop, count);
                    break;
                default:
                    throw new Exception("no such type");
            }
        }

        public void ResetPopAllocation()
        {
            AvailablePopulation = TotalPopulation;
            SiliconPop = 0;
            CopperPop = 0;
            IronPop = 0;
            AluminumPop = 0;
            ElectronicPop = 0;
            IndustrailPop = 0;
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
                    switch (type)
                    {
                        case GameResourceType.Silicon:
                            if (SiliconPop > 0)
                            {
                                SiliconPop--;
                            }
                            else
                            {
                                i--;
                            }
                            break;
                        case GameResourceType.Copper:
                            if (CopperPop > 0)
                            {
                                CopperPop--;
                            }
                            else
                            {
                                i--;
                            }
                            break;
                        case GameResourceType.Iron:
                            if (IronPop > 0)
                            {
                                IronPop--;
                            }
                            else
                            {
                                i--;
                            }
                            break;
                        case GameResourceType.Aluminum:
                            if (AluminumPop > 0)
                            {
                                AluminumPop--;
                            }
                            else
                            {
                                i--;
                            }
                            break;
                        case GameResourceType.Electronic:
                            if (ElectronicPop > 0)
                            {
                                ElectronicPop--;
                            }
                            else
                            {
                                i--;
                            }
                            break;
                        case GameResourceType.Industrial:
                            if (IndustrailPop > 0)
                            {
                                IndustrailPop--;
                            }
                            else
                            {
                                i--;
                            }
                            break;
                        default:
                            throw new Exception("no such type");
                    }
                }
                AvailablePopulation = 0;
            }
        }
        private int TrySet(int onPositionPop, int count)
        {
            if (count >= onPositionPop)
            {
                int newAllocate = count - onPositionPop;
                if (newAllocate > AvailablePopulation)
                {
                    throw new InvalidPopAllocException();
                }
                else
                {
                    onPositionPop = count;
                    AvailablePopulation -= (newAllocate);
                }
            }
            else
            {
                if (count < 0)
                {
                    throw new InvalidPopAllocException();
                }
                else
                {
                    onPositionPop = count;
                    AvailablePopulation += (onPositionPop - count);
                }
            }

            return onPositionPop;
        }

        private int index = -1;
        private int GetNextIndex()
        {
            index++;
            return (index %= 6);
        }
    }
}
