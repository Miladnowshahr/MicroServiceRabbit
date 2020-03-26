using MicroRabbit.Presentation.MVC.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Presentation.MVC.Services
{
    public interface ITransferServices
    {
        Task Transfer(TransferDto transferDto);

    }
}
