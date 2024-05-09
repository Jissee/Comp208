namespace EoE
{
    /// <summary>
    /// A interface indicates the task executed after every turn.
    /// </summary>
    public interface ITickable
    {
        void Tick();
    }
}
