using System.Collections.Generic;

namespace MiniBank.Models
{
    public abstract class Account
    {
        public virtual int Id { get; set; }
        public virtual decimal Balance { get; set; }
        public virtual IList<User> Users { get; set; } = new List<User>();
    }
}
