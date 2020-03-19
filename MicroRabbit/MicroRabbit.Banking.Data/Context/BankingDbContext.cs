using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Banking.Data.Context
{
    public class BankingDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-RNVP9U0;database=BankingDb;Integrated Security=True");
        }


        public DbSet<Account> Accounts  { get; set; }          
    }
}
