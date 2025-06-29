using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using MiniBank.Mappings;
using System;

namespace MiniBank.DB
{
    public class NHibernateHelper : IDisposable
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateHelper(string server, string database, string username, string password)
        {
            _sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                    .ConnectionString(c => c
                        .Server(server)
                        .Database(database)
                        .Username(username)
                        .Password(password)
                    )
                )
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<AccountMap>()
                )
                .BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }

        public void Dispose()
        {
            _sessionFactory?.Dispose();
        }
    }
}
