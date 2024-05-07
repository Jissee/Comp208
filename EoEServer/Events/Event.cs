using EoE.Network.Entities;

namespace EoE.Server.Events
{
    public delegate void EventCallBack(IServer server, IPlayer player);
    public class Event : ITickable
    {
        private bool started = false;
        private IServer server;
        private IPlayer player;
        private EventCallBack eventCallBack;
        private Predicate<IPlayer> playerCondition;
        private Predicate<IServer> serverCondition;
        private int happenIn;
        private int remainingTicks;

        public void Tick()
        {
            if (happenIn > 0)
            {
                happenIn--;
                return;
            }
            else
            {
                started = true;
            }
            

            if (started)
            {
                if (remainingTicks > 0)
                {
                    remainingTicks--;
                    if (player != null &&
                        !player.IsLose &&
                        playerCondition(player) &&
                        serverCondition(server)
                    )
                    {
                        eventCallBack(server, player);
                    }
                }
            }
        }
        public bool NeedRemove()
        {
            return started && remainingTicks == 0;
        }
        public class Builder
        {
            Event tmp;
            public Builder()
            {
                tmp = new Event();
            }

            public Builder ForServer(IServer server)
            {
                tmp.server = server;
                return this;
            }
            public Builder ForPlayer(IPlayer player)
            {
                tmp.player = player;
                tmp.server = player.Server;
                return this;
            }
            public Builder HappenIn(int ticks)
            {
                if (ticks <= 0)
                {
                    throw new ArgumentException("Ticks must be larger than 0.");
                }
                tmp.happenIn = ticks;
                return this;
            }
            public Builder LastFor(int ticks)
            {
                if (ticks <= 0)
                {
                    throw new ArgumentException("Ticks must be larger than 0.");
                }
                tmp.remainingTicks = ticks;
                return this;
            }
            public Builder IfPlayer(Predicate<IPlayer> playerCondition)
            {
                tmp.playerCondition = playerCondition;
                return this;
            }
            public Builder IfServer(Predicate<IServer> serverCondition)
            {
                tmp.serverCondition = serverCondition;
                return this;
            }

            public Builder Do(EventCallBack eventCallBack)
            {
                tmp.eventCallBack = eventCallBack;
                return this;
            }
            public Event Build()
            {
                if (tmp.server != null)
                {
                    if (tmp.eventCallBack != null)
                    {
                        if (tmp.serverCondition != null)
                        {
                            if (tmp.playerCondition != null)
                            {
                                return tmp;
                            }
                        }
                    }
                }
                throw new Exception("Incomplete event.");
            }
        }
    }
}
