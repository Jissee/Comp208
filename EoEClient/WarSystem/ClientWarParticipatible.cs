namespace EoE.Client.WarSystem
{
    public class ClientWarParticipatible
    {
        public List<string> Participators { get; set; }
        public ClientWarParticipatible(string[] names)
        {
            Participators = [.. names];
        }
    }
}
