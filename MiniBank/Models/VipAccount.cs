namespace MiniBank.Models
{
    public class VipAccount : Account 
    {
        public override void Withdraw(decimal amount)
        {
            Balance -= amount;
        }
    }
}
