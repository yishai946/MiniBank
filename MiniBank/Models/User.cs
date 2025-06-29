using System.Collections.Generic;

namespace MiniBank.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Account> Accounts { get; set; } = new List<Account>();

        public virtual void AddAccount(Account account)
        {
            Accounts.Add(account);

            if (!account.Users.Contains(this))
            {
                account.Users.Add(this);
            }
        }
    }

}
