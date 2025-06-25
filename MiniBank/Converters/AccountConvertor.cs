using MiniBank.Models;
using MiniBank.Models.Enums;
using System;
using System.Collections.Generic;

namespace MiniBank.Converters
{
    public class AccountConvertor
    {
        public delegate Account CreateAccount(int id, decimal balance);

        private readonly Dictionary<AccountType, CreateAccount> accountRegistry =
            new Dictionary<AccountType, CreateAccount>()
            {
                {
                    AccountType.Simple,
                    (id, balance) => new SimpleAccount { Id = id, Balance = balance }
                },
                {
                    AccountType.VIP,
                    (id, balance) => new VipAccount { Id = id, Balance = balance }
                }
            };

        public Account Convert(AccountType type, int id, decimal balance) => accountRegistry[type](id, balance);
    }
}
