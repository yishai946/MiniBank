using System;

namespace MiniBank.Models
{
    public class SimpleAccount : Account
    {
        public override void Withdraw(decimal amount)
        {
            if (Balance < amount)
            {
                throw new ArgumentException(Strings.InsufficientBalance);
            }

            Balance -= amount;
        }
    }
}
