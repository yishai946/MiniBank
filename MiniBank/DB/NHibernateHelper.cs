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
                    .ShowSql()
                )
                .Mappings(m => m.FluentMappings
                    .Add<UserMap>()
                    .Add<AccountMap>()
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

        #region wrappers
        public void WithTransaction(Action<ISession> action)
        {
            using (var session = _sessionFactory.OpenSession())
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
            using (var session = _sessionFactory.OpenSession())
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
            using (var session = _sessionFactory.OpenSession())
            {
                return func(session);
            }
        }
        #endregion
    }
}
