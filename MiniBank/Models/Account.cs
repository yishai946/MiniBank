using System.Collections.Generic;

namespace MiniBank.Models
{
    public abstract class Account
    {
        public virtual int Id { get; set; }
        public virtual decimal Balance { get; set; }
        public virtual IList<User> Users { get; set; } = new List<User>();

        public virtual void Deposit(decimal amount) => Balance += amount;

        public abstract void Withdraw(decimal amount);

        public virtual void AddUser(User user)
        {
            Users.Add(user);

            if (!user.Accounts.Contains(this))
            {
                user.Accounts.Add(this);
            }
        }
    }
}
