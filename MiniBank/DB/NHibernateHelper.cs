using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MiniBank.Models;
using NHibernate;
using System;

namespace MiniBank.DB
{
    public class NHibernateHelper : IDisposable
    {
        private readonly ISessionFactory SessionFactory;

        public NHibernateHelper(string server, string database, string username, string password)
        {
            var autoMap = AutoMap
                .AssemblyOf<User>(new AutoMappingConfiguration())
                .UseOverridesFromAssemblyOf<User>();

            SessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                    .ConnectionString(connection => connection
                        .Server(server)
                        .Database(database)
                        .Username(username)
                        .Password(password)
                    )
                )
                .Mappings(map =>
                {
                    map.AutoMappings.Add(autoMap);
                    map.FluentMappings.AddFromAssemblyOf<Account>();
                })
                .BuildSessionFactory();
        }

        private ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public void Dispose()
        {
            SessionFactory?.Dispose();
        }

        #region wrappers
        public void WithTransaction(Action<ISession> action)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    action(session);
                    transaction.Commit();
                }
            }
        }

        public T WithTransaction<T>(Func<ISession, T> func)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var result = func(session);
                    transaction.Commit();

                    return result;
                }
            }
        }

        public T WithSession<T>(Func<ISession, T> func)
        {
            using (var session = OpenSession())
            {
                return func(session);
            }
        }
        #endregion
    }
}
