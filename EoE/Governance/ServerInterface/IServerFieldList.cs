using EoE.Governance.Interface;

namespace EoE.Governance.ServerInterface
{
    /// <summary>
    /// The server side interface of field list.
    /// </summary>
    public interface IServerFieldList : IFieldList
    {
        void ClearAll();
    }
}
