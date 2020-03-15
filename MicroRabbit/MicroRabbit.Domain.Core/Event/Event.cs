using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Domain.Core.Event
{
    public abstract class Event
    {
        public DateTime Timestap { get; protected set; }


        public Event()
        {
            Timestap = DateTime.Now;
        }
    }
}
