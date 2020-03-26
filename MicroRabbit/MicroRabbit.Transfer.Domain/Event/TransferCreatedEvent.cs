using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Transfer.Domain.Event
{
    public class TransferCreatedEvent:MicroRabbit.Domain.Core.Event.Event
    {
        public int From { get; private set; }

        public int To { get; private set; }

        public decimal Amount { get; private set; }

        public TransferCreatedEvent(int from,int to,decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}
