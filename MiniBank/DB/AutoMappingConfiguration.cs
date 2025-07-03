using FluentNHibernate;
using FluentNHibernate.Automapping;
using MiniBank.Models;
using System;

namespace MiniBank.DB
{
    public class AutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type == typeof(User);
        }

        public override bool IsId(Member member)
        {
            return member.Name == Strings.IdVarName;
        }
    }
}
