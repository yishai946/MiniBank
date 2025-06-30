using System;

namespace MiniBank.Models
{
    public class SimpleAccount : Account
    {
        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(Strings.AmountErrorMsg);
            }

            if (Balance < amount)
            {
                throw new ArgumentException(Strings.InsufficientBalance);
            }

            Balance -= amount;
        }
    }
}
