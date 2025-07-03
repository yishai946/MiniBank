using FluentNHibernate.Mapping;
using MiniBank.Models;

namespace MiniBank.Mappings
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Table(Strings.AccountsTableName);
            Id(account => account.Id).GeneratedBy.Identity();
            Map(account => account.Balance)
                .Not.Nullable();

            HasManyToMany(account => account.Users)
                .Table(Strings.UserAccountsTableName)
                .ParentKeyColumn(Strings.AccountIdVarName)
                .ChildKeyColumn(Strings.UserIdVarName)
                .Inverse();

            DiscriminateSubClassesOnColumn(Strings.TypeVarName);
        }
    }
}
