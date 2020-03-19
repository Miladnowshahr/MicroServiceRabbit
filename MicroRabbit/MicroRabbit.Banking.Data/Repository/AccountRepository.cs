using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Banking.Data.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private  BankingDbContext _db;

        public AccountRepository(BankingDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _db.Accounts;
        }
    }
}
