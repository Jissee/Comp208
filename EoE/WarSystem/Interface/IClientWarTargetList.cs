using EoE.Server.WarSystem;

namespace EoE.WarSystem.Interface
{
    public interface IClientWarTargetList
    {
        void ChangeClaim(string name, WarTarget warTarget);
    }
}
