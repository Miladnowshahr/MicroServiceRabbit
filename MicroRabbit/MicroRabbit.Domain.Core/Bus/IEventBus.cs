using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MicroRabbit.Domain.Core.Command;
using MicroRabbit.Domain.Core.Event;
namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command.Command;

        void Publish<T>(T @event) where T : Event.Event;

        void Subscibe<T, TH>()
            where T : Event.Event
            where TH : IEventHandler<T>;
    }
}
