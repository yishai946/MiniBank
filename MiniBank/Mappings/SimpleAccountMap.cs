using FluentNHibernate.Mapping;
using MiniBank.Models;

namespace MiniBank.Mappings
{
    public class SimpleAccountMap : SubclassMap<SimpleAccount>
    {
        public SimpleAccountMap()
        {
            DiscriminatorValue(Strings.SimpleVarName);
        }
    }
}
