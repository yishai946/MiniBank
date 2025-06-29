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

            DiscriminateSubClassesOnColumn(Strings.TypeVarName);

            HasManyToMany(account => account.Users)
                .Table(Strings.UserAccountsTableName)
                .ParentKeyColumn(Strings.AccountIdVarName)
                .ChildKeyColumn(Strings.UserIdVarName)
                .Inverse()
                .Cascade.All();
        }
    }
}
