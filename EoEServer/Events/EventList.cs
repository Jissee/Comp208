using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Events
{
    public class EventList : ITickable
    {
        private List<Event> events = new List<Event>();
        public EventList() { }
        public void AddEvent(Event e) 
        {
            events.Add(e);
        }
        public void Tick()
        {
            List<Event> removal = new List<Event>();
            foreach (Event e in events)
            {
                e.Tick();
                if (e.NeedRemove())
                {
                    removal.Add(e);
                }
            }
            events.RemoveAll(removal.Contains);
        }
    }
}
