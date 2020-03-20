using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Banking.Domain.Command;
using MicroRabbit.Banking.Domain.Event;
using MicroRabbit.Domain.Core.Bus;

namespace MicroRabbit.Banking.Domain.CommandHandlers
{
    public class TransferCommandHandler:IRequestHandler<CreateTransferCommand,bool>
    {
        private readonly IEventBus _bus;

        public TransferCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }


        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            //public event to rabbitMQ
            _bus.Publish(new TransferCreatedEvent(request.From, request.To, request.Amount));
            return Task.FromResult(true);
        }
    }
}
