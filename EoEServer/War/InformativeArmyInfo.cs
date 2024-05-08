namespace EoE.Server.War
{
    public class InformativeArmyInfo : ArmyInfo
    {
        public override int Worth => 5;
        public override int MechanAttack => 1;

        public override int MechanDefense => 2;

        public override int BattleAttack => 0;
    }
}
