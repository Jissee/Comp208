using EoE.GovernanceSystem;

namespace EoE
{
    public class GameStatus : ITickable
    {
        public int TotalTick { get; set; }
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
        public Modifier GlobalIndustralModifier { get; init; }

        public GameStatus(int initialUndentifiedField, int count)
        {
            GlobalResourceModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalPrimaryModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalSecondaryModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalSiliconModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalCopperModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalIronModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalAluminumModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalElectronicModifier = new Modifier("", Modifier.ModifierType.Plus);
            GlobalIndustralModifier = new Modifier("", Modifier.ModifierType.Plus);

            GlobalPrimaryModifier.AddNode(GlobalResourceModifier);
            GlobalSecondaryModifier.AddNode(GlobalResourceModifier);

            GlobalSiliconModifier.AddNode(GlobalPrimaryModifier);
            GlobalCopperModifier.AddNode(GlobalPrimaryModifier);
            GlobalIronModifier.AddNode(GlobalPrimaryModifier);
            GlobalAluminumModifier.AddNode(GlobalPrimaryModifier);

            GlobalElectronicModifier.AddNode(GlobalSecondaryModifier);
            GlobalIndustralModifier.AddNode(GlobalSecondaryModifier);

            TickCount = 0;
            TotalTick = count;
            UnidentifiedField = initialUndentifiedField;
        }
        public void Tick()
        {
            TickCount++;
        }
    }
}
