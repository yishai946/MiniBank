using MiniBank.Models;
using MiniBank.Models.Enums;
using System;
using System.Collections.Generic;

namespace MiniBank.Views
{
    public class AccountView
    {
        private readonly UtilView _utilView = new UtilView();

        public void ShowAccounts(List<Account> accounts)
        {
            _utilView.Output(Strings.AccountsViewHeader);

            accounts.ForEach(account => ShowAccount(account));
        }

        public void ShowAccount(Account account) =>
            _utilView.Output(string.Format(
                Strings.AccountFormat,
                account.Id,
                account.GetType().Name,
                account.Balance
            ));

        public AccountType GetAccountType()
        {
            _utilView.Output(Strings.AccountTypeInputMsg);

            foreach (var accountType in Enum.GetValues(typeof(AccountType)))
            {
                _utilView.Output(string.Format(Strings.MenuItemFormat, (int)accountType, accountType));
            }

            var choosenAccount = _utilView.GetInput();

            if (int.TryParse(choosenAccount, out int value) && Enum.IsDefined(typeof(AccountType), value))
            {
                return (AccountType)value;
            }

            _utilView.Output(Strings.ChoiceOutOfRange);

            return GetAccountType();
        }
    }
}
