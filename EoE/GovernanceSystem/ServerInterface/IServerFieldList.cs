using EoE.GovernanceSystem.Interface;

namespace EoE.GovernanceSystem.ServerInterface
{
    public interface IServerFieldList : IFieldList
    {

        void Filedconversion(FieldStack origin, FieldStack converted);
        void Filedconversion(GameResourceType originalType, int originalcount, GameResourceType convertedType, int convertedCount);
        void ClearAll();
    }
}
