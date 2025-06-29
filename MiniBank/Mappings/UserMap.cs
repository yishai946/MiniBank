using FluentNHibernate.Mapping;
using MiniBank.Models;

namespace MiniBank.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table(Strings.UsersTableName);
            Id(user => user.Id).GeneratedBy.Identity();
            Map(user => user.Name);

            HasManyToMany(user => user.Accounts)
                .Table(Strings.UserAccountsTableName)
                .ParentKeyColumn(Strings.UserIdVarName)
                .ChildKeyColumn(Strings.AccountIdVarName)
                .Cascade.All();
        }
    }

}
