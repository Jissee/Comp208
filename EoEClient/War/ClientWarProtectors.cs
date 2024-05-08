namespace EoE.Client.War
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
