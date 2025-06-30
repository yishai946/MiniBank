using System;

namespace MiniBank.Models
{
    public class VipAccount : Account 
    {
        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(Strings.AmountErrorMsg);
            }

            Balance -= amount;
        }
    }
}
