using MiniBank.DB;
using MiniBank.Models;
using MiniBank.Services;
using System.Collections.Generic;
using System;
using MiniBank.Views;

namespace MiniBank.Controllers
{
    public class AccountController
    {
        private readonly NHibernateHelper _nhHelper;
        private readonly AccountFactory _accountFactory = new AccountFactory();
        private readonly UtilView _utilView = new UtilView();
        private readonly AccountView _accountView = new AccountView();

        public AccountController(NHibernateHelper nhHelper)
        {
            _nhHelper = nhHelper;
        }

        public void Withdraw()
        {
            var accountId = int.Parse(_utilView.GetInput(Strings.AccountIdInputMsg));
            var amount = decimal.Parse(_utilView.GetInput(Strings.AmountInputMsg));

            ExecuteAccountAction(accountId, account => account.Withdraw(amount));

            _utilView.Output(string.Format(Strings.WithdrawMsg, amount, accountId));
        }

        public void Deposit()
        {
            var accountId = int.Parse(_utilView.GetInput(Strings.AccountIdInputMsg));
            var amount = decimal.Parse(_utilView.GetInput(Strings.AmountInputMsg));

            ExecuteAccountAction(accountId, account => account.Deposit(amount));

            _utilView.Output(string.Format(Strings.DepositMsg, amount, accountId));
        }

        private void ExecuteAccountAction(int accountId, Action<Account> accountAction) =>
            _nhHelper.WithTransaction(session =>
            {
                var account = session.Get<Account>(accountId)
                    ?? throw new KeyNotFoundException(string.Format(Strings.AccountNotFound, accountId));

                accountAction(account);
            });

        public void CreateAccount()
        {
            var userId = int.Parse(_utilView.GetInput(Strings.UserIdInputMsg));
            var type = _accountView.GetAccountType();

            var newAccountId = _nhHelper.WithTransaction(session =>
            {
                var user = session.Get<User>(userId)
                    ?? throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));

                var newAccount = _accountFactory.CreateAccount(type);
                user.AddAccount(newAccount);

                return newAccount.Id;
            });

            _utilView.Output(string.Format(Strings.AccountCreatedMsg, newAccountId));
        }

        public void DeleteAccount()
        {
            var accountId = int.Parse(_utilView.GetInput(Strings.AccountIdInputMsg));

            _nhHelper.WithTransaction(session =>
            {
                var account = session.Get<Account>(accountId)
                    ?? throw new KeyNotFoundException(string.Format(Strings.AccountNotFound, accountId));

                session.Delete(account);
            });

            _utilView.Output(string.Format(Strings.AccountDeletedMsg, accountId));
        }
    }
}
