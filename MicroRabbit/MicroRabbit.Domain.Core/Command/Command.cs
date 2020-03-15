using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Domain.Core.Command
{
    public abstract class Command:Message
    {

        public DateTime Timestap { get; protected set };

        protected Command()
        {
            Timestap = DateTime.Now;
        }
    }
}
