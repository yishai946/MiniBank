using MiniBank.Models;
using MiniBank.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniBank.Views
{
    public class AccountView
    {

        public void ShowAccounts(List<Account> accounts)
        {
            new ApplicationView().Output(Strings.AccountsViewHeader);

            accounts.ForEach(account => ShowAccount(account));
        }

        public void ShowAccount(Account account) =>
            new ApplicationView().Output(string.Format(
                Strings.AccountFormat,
                account.Id,
                account.GetType().Name,
                account.Balance
            ));

        public AccountType GetAccountType()
        {
            new ApplicationView().Output(Strings.AccountTypeInputMsg);

            Enum.GetValues(typeof(AccountType))
                .Cast<AccountType>()
                .ToList()
                .ForEach(accountType => 
                    new ApplicationView().Output(string.Format(Strings.MenuItemFormat, (int)accountType, accountType))
                );

            var choosenAccount = new ApplicationView().GetString();

            if (int.TryParse(choosenAccount, out int value) && Enum.IsDefined(typeof(AccountType), value))
            {
                return (AccountType)value;
            }

            new ApplicationView().Output(Strings.ChoiceOutOfRange);

            return GetAccountType();
        }
    }
}
