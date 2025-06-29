using MiniBank.Models.Enums;
using MiniBank.Models;
using System;
using System.Collections.Generic;

namespace MiniBank.Services
{
    public class AccountFactory
    {
        private readonly Dictionary<AccountType, Func<Account>> creators;

        public AccountFactory()
        {
            creators = new Dictionary<AccountType, Func<Account>>()
            {
                { AccountType.Simple, () => new SimpleAccount { Balance = 0m } },
                { AccountType.VIP, () => new VipAccount { Balance = 0m } },
            };
        }

        public Account CreateAccount(AccountType type)
        {
            if (!creators.TryGetValue(type, out var creator))
            {
                throw new ArgumentException(string.Format(Strings.UnsoppertedAccount, type));
            }

            return creator();
        }
    }

}
