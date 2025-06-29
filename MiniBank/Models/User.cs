using System.Collections.Generic;

namespace MiniBank.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Account> Accounts { get; set; } = new List<Account>();
    }

}
