using MiniBank.DB;
using MiniBank.Models;
using MiniBank.Services;
using System.Collections.Generic;
using System;
using MiniBank.Views;
using MiniBank.Logic;

namespace MiniBank.Controllers
{
    public class AccountController
    {
        private readonly NHibernateHelper NhHelper;

        public AccountController(NHibernateHelper nhHelper)
        {
            NhHelper = nhHelper;
        }

        public void Withdraw()
        {
            var accountId = new ApplicationView().GetId(Strings.AccountVarName);
            var amount = new ApplicationView().GetAmount();

            ModifyAccount(accountId, account => new AccountLogic().Withdraw(account, amount));

            new ApplicationView().Output(string.Format(Strings.WithdrawMsg, amount, accountId));
        }

        public void Deposit()
        {
            var accountId = new ApplicationView().GetId(Strings.AccountVarName);
            var amount = new ApplicationView().GetAmount();

            ModifyAccount(accountId, account => new AccountLogic().Deposit(account, amount));

            new ApplicationView().Output(string.Format(Strings.DepositMsg, amount, accountId));
        }

        private void ModifyAccount(int accountId, Action<Account> accountAction) =>
            NhHelper.WithTransaction(session =>
            {
                var account = session.Get<Account>(accountId);
                
                if (account == null) {
                    throw new KeyNotFoundException(string.Format(Strings.AccountNotFound, accountId));
                }
                
                accountAction(account);
            });

        public void CreateAccount()
        {
            var userId = new ApplicationView().GetId(Strings.AccountVarName);
            var type = new AccountView().GetAccountType();

            var newAccountId = NhHelper.WithTransaction(session =>
            {
                var user = session.Get<User>(userId);

                if (user == null) {
                    throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));
                }

                var newAccount = new AccountFactory().CreateAccount(type);

                user.Accounts.Add(newAccount);
                newAccount.Users.Add(user);

                session.Save(newAccount);

                return newAccount.Id;
            });

            new ApplicationView().Output(string.Format(Strings.AccountCreatedMsg, newAccountId));
        }

        public void DeleteAccount()
        {
            var accountId = new ApplicationView().GetId(Strings.AccountVarName);

            NhHelper.WithTransaction(session =>
            {
                var account = session.Get<Account>(accountId);

                if (account == null) {
                    throw new KeyNotFoundException(string.Format(Strings.AccountNotFound, accountId));
                }

                session.Delete(account);
            });

            new ApplicationView().Output(string.Format(Strings.AccountDeletedMsg, accountId));
        }
    }
}
