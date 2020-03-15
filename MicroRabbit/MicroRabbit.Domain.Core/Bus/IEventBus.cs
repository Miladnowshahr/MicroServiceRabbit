using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Publish<T>(T @event) where T : Event;

        void Subscibe<T, Th>()
            where T : Event
            where TH : IEventHandler<T>;
    }
}
