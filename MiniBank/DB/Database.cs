using DotNetEnv;
using System;

namespace MiniBank.DB
{
    public class Database
    {
        private static readonly Lazy<NHibernateHelper> LazyInstance = new Lazy<NHibernateHelper>(() =>
        {
            Env.Load();

            var host = Environment.GetEnvironmentVariable(Strings.HostVarName);
            var database = Environment.GetEnvironmentVariable(Strings.DBVarName);
            var username = Environment.GetEnvironmentVariable(Strings.UsernameVarName);
            var password = Environment.GetEnvironmentVariable(Strings.PasswordVarName);

            return new NHibernateHelper(host, database, username, password);
        });

        public static NHibernateHelper Instance => LazyInstance.Value;
    }
}
