using System;
using MiniBank.Models;

namespace MiniBank.Logic
{
    public class AccountLogic
    {
        public void Deposit(Account account, decimal amount)
        {
            account.Balance += amount;
        }

        public void Withdraw(Account account, decimal amount)
        {
            Console.WriteLine(account.GetType().Name);
            EnsureSufficientBalance(account, amount);

            account.Balance -= amount;
        }

        private void EnsureSufficientBalance(Account account, decimal amount)
        {
            if (account.GetType().Name == Strings.SimpleVarName && amount > account.Balance)
            {
                throw new ArgumentException(Strings.InsufficientBalance);
            }
        }
    }
}
