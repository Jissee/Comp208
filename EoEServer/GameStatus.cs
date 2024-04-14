using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class GameStatus : ITickable
    {
        public int TotalTick { get; private set; }
        public int TickCount { get; private set; }
        public int UnidentifiedField { get; set; }
        public Modifier GlobalResourceModifier { get; init; }
        public Modifier GlobalPrimaryModifier { get; init; }
        public Modifier GlobalSecondaryModifier { get; init; }
        public Modifier GlobalSiliconModifier { get; init; }
        public Modifier GlobalCopperModifier { get; init; }
        public Modifier GlobalIronModifier { get; init; }
        public Modifier GlobalAluminumModifier { get; init; }
        public Modifier GlobalElectronicModifier { get; init; }
        public Modifier GlobalIndustryModifier { get; init; }

        public GameStatus(int initialUndentifiedField, int totalTick)
        {
            GlobalResourceModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalPrimaryModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalSecondaryModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalSiliconModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalCopperModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalIronModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalAluminumModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalElectronicModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalIndustryModifier = new Modifier("", Modifier.ModifierType.Plus);
            TickCount = 0;
            TotalTick = totalTick;
            UnidentifiedField = initialUndentifiedField;
        }
        public void Tick()
        {
            throw new NotImplementedException();
        }

        public void SetTotalTick(int totalTick)
        {
            this.TotalTick = totalTick;
        }
    }
}
