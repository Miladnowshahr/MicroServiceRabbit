using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Data.Repository
{
    public class TransferRepository:ITransferRepository
    {
        private TransferDbContext _db;
        public TransferRepository(TransferDbContext db)
        {
            _db = db;
        }
        public void Add(TransferLog transferLog)
        {
            _db.TransferLogs.Add(transferLog);
            _db.SaveChanges();
        }
        public IEnumerable<TransferLog> GeTransferLogs()
        {
            return _db.TransferLogs;
        }
    }
}
