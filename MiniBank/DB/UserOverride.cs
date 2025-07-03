using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Automapping;
using MiniBank.Models;

namespace MiniBank.DB
{
    public class UserOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.Table(Strings.UsersTableName);

            mapping.HasManyToMany(x => x.Accounts)
                .Table(Strings.UserAccountsTableName)
                .ParentKeyColumn(Strings.UserIdVarName)
                .ChildKeyColumn(Strings.AccountIdVarName)
                .Cascade.All();
        }
    }
}
