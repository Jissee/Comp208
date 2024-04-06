﻿using EoE.GovernanceSystem;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public class PlayerFieldList
    {
        public FieldStack CountryFieldSilicon { get; init;}
        public FieldStack CountryFieldCopper{get; init;}
        public FieldStack CountryFieldIron{get; init;}
        public FieldStack CountryFieldAluminum{get; init;}
        public FieldStack CountryFieldElectronic{get; init;}
        public FieldStack CountryFieldIndustry{get; init;}

        public int SiliconPop { get; private set; }
        public int CopperPop { get;private set;}
        public int IronPop {get;private set;}
        public int AluminumPop {get;private set;}
        public int ElectronicPop {get;private set;}
        public int IndustrailPop {get;private set;}

        public int AvailablePopulation { get; private set; }

        public int TotalPopulation => SiliconPop + CopperPop + 
            IronPop + AluminumPop + ElectronicPop + 
            IndustrailPop + AvailablePopulation;

        public int TotalFieldCount => CountryFieldSilicon.Count + CountryFieldCopper.Count + 
            CountryFieldAluminum.Count + CountryFieldIron.Count + 
            CountryFieldElectronic.Count + CountryFieldIndustry.Count;
        public PlayerFieldList()
        {
            CountryFieldSilicon = new FieldStack(GameResourceType.Silicon, 20);
            CountryFieldCopper = new FieldStack(GameResourceType.Copper, 20);
            CountryFieldIron = new FieldStack(GameResourceType.Iron, 20);
            CountryFieldAluminum = new FieldStack(GameResourceType.Aluminum, 20);
            CountryFieldElectronic = new FieldStack(GameResourceType.Electronic, 20);
            CountryFieldIndustry = new FieldStack(GameResourceType.Industrial, 20);

            SiliconPop = 0;
            CopperPop = 0;
            IronPop = 0;
            AluminumPop = 0;
            ElectronicPop = 0;
            IndustrailPop = 0;
            AvailablePopulation = 100;
        }

        public void addField(FieldStack adder)
        {
            GameResourceType type = adder.Type;
            switch (type)
            {
                case GameResourceType.Silicon:
                    CountryFieldSilicon.Add(adder);
                    break;
                case GameResourceType.Copper:
                    CountryFieldCopper.Add(adder);
                    break;
                case GameResourceType.Iron:
                    CountryFieldIron.Add(adder);
                    break;
                case GameResourceType.Aluminum:
                    CountryFieldAluminum.Add(adder);
                    break;
                case GameResourceType.Electronic:
                    CountryFieldElectronic.Add(adder);
                    break;
                case GameResourceType.Industrial:
                    CountryFieldIndustry.Add(adder);
                    break;
                default:
                    throw new Exception("no such type");
            }
        }

        public FieldStack SplitFidle(GameResourceType type, int count)
        {
           return SplitFidle(new FieldStack(type, count));
        }
        public FieldStack SplitFidle(FieldStack field)
        {
            GameResourceType type = field.Type;
            switch (type)
            {
                case GameResourceType.Silicon:
                    return CountryFieldSilicon.Split(field.Count);
                case GameResourceType.Copper:
                    return CountryFieldCopper.Split(field.Count);
                case GameResourceType.Iron:
                    return CountryFieldIron.Split(field.Count);
                case GameResourceType.Aluminum:
                    return CountryFieldAluminum.Split(field.Count);
                case GameResourceType.Electronic:
                    return CountryFieldElectronic.Split(field.Count);
                case GameResourceType.Industrial:
                    return CountryFieldIndustry.Split(field.Count);
                default:
                    throw new Exception("no such type");
            }
        }

        public int GetFieldCount(GameResourceType type)
        {
            switch (type)
            {
                case GameResourceType.Silicon:
                    return CountryFieldSilicon.Count;
                case GameResourceType.Copper:
                    return CountryFieldCopper.Count;
                case GameResourceType.Iron:
                    return CountryFieldIron.Count;
                case GameResourceType.Aluminum:
                    return CountryFieldAluminum.Count;
                case GameResourceType.Electronic:
                    return CountryFieldElectronic.Count;
                case GameResourceType.Industrial:
                    return CountryFieldIndustry.Count;
                default:
                    throw new Exception("no such type");
            }
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
            }
        }
        private int TrySet(int onPositionPop, int count)
        {
            if (count >= 0)
            {
                if (count > AvailablePopulation)
                {
                    throw new InvalidPopAllocException();
                }
                else
                {
                    onPositionPop += count;
                    AvailablePopulation -= count;
                }
            }
            else
            {
                if (-count >= onPositionPop)
                {
                    throw new InvalidPopAllocException();
                }
                else
                {
                    onPositionPop += count;
                    AvailablePopulation += count;
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