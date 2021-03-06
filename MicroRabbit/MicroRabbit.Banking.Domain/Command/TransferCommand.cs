﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Banking.Domain.Command
{
    public abstract class TransferCommand:MicroRabbit.Domain.Core.Command.Command
    {
        public int From { get; protected set; }
        public int To { get; protected set; }

        public decimal Amount { get; protected set; }
    }
}
