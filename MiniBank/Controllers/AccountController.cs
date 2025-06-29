using MiniBank.DB;
using MiniBank.Models;
using MiniBank.Services;
using System.Collections.Generic;
using System;
using MiniBank.Models.Enums;

namespace MiniBank.Controllers
{
    public class AccountController
    {
        private readonly AccountFactory _accountFactory;
        private readonly NHibernateHelper _nhHelper;

        public AccountController(NHibernateHelper nhHelper)
        {
            _accountFactory = new AccountFactory();
            _nhHelper = nhHelper;
        }

        public void Withdraw(int accountId, decimal amount) =>
            ExecuteAccountAction(accountId, account => account.Withdraw(amount));

        public void Deposit(int accountId, decimal amount) =>
            ExecuteAccountAction(accountId, account => account.Deposit(amount));

        private void ExecuteAccountAction(int accountId, Action<Account> accountAction) =>
            _nhHelper.WithTransaction(session =>
            {
                var account = session.Get<Account>(accountId)
                    ?? throw new KeyNotFoundException(string.Format(Strings.AccountNotFound, accountId));

                accountAction(account);
            });

        public int CreateAccount(int userId, AccountType type) =>
            _nhHelper.WithTransaction(session =>
            {
                var user = session.Get<User>(userId)
                    ?? throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));

                var newAccount = _accountFactory.CreateAccount(type);
                user.AddAccount(newAccount);

                return newAccount.Id;
            });

        public void DeleteAccount(int accountId) =>
            _nhHelper.WithTransaction(session =>
            {
                var account = session.Get<Account>(accountId)
                    ?? throw new KeyNotFoundException(string.Format(Strings.AccountNotFound, accountId));

                session.Delete(account);
            });
    }
}
