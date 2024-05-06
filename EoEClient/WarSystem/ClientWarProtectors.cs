namespace EoE.Client.WarSystem
{
    public class ClientWarProtectors
    {
        List<string> ProtectorsName { get; set; }
        public ClientWarProtectors(string[] names)
        {
            ProtectorsName = [.. names];
        }
    }
}
