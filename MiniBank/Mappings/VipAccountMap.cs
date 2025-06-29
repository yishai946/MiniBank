using FluentNHibernate.Mapping;
using MiniBank.Models;

namespace MiniBank.Mappings
{
    public class VipAccountMap : SubclassMap<VipAccount>
    {
        public VipAccountMap()
        {
            DiscriminatorValue(Strings.VipVarName);
        }
    }
}
