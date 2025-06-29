using MiniBank.DB;
using MiniBank.Models;
using MiniBank.Models.Enums;
using MiniBank.Services;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniBank.Controllers
{
    public class UserController
    {
        private readonly AccountFactory _accountFactory;
        private readonly NHibernateHelper _nhHelper;

        public UserController(NHibernateHelper nhHelper)
        {
            _accountFactory = new AccountFactory();
            _nhHelper = nhHelper;
        }

        public List<User> GetAllUsers() =>
            _nhHelper.WithSession(session => session.Query<User>().ToList());

        public List<Account> GetUserAccounts(int userId) =>
            _nhHelper.WithSession(session =>
            {
                var user = session.Get<User>(userId)
                    ?? throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));

                return user.Accounts.ToList();
            });

        public int CreateUser(string name) =>
            _nhHelper.WithTransaction(session =>
            {
                var newUser = new User { Name = name };
                session.Save(newUser);

                return newUser.Id;
            });

        public void DeleteUser(int userId) =>
            _nhHelper.WithTransaction(session =>
            {
                var user = session.Get<User>(userId)
                    ?? throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));
                
                session.Delete(user);
            });
    }
}
