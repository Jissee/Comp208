namespace EoE.Client.War
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
