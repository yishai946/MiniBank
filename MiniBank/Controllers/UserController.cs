using MiniBank.DB;
using MiniBank.Models;
using MiniBank.Services;
using MiniBank.Views;
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
        private readonly UserView _userView = new UserView();
        private readonly AccountView _accountView = new AccountView();
        private readonly UtilView _utilView = new UtilView();

        public UserController(NHibernateHelper nhHelper)
        {
            _accountFactory = new AccountFactory();
            _nhHelper = nhHelper;
        }

        public void GetAllUsers()
        {
            var users = _nhHelper.WithSession(session => session.Query<User>().ToList());

            _userView.ShowUsers(users);
        }

        public void GetUserAccounts() {
            var userId = int.Parse(_utilView.GetInput(Strings.UserIdInputMsg));

            var accounts = 
                _nhHelper.WithSession(session =>
                {
                    var user = session.Get<User>(userId)
                        ?? throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));

                    return user.Accounts.ToList();
                });

            _accountView.ShowAccounts(accounts);
        }

        public void CreateUser()
        {
            var name = _utilView.GetInput(Strings.NameInputMsg);

            var newUserId = 
                _nhHelper.WithTransaction(session =>
                {
                    var newUser = new User { Name = name };
                    session.Save(newUser);

                    return newUser.Id;
                }
            );

            _utilView.Output(string.Format(Strings.UserCreatedMsg, newUserId));
        }

        public void DeleteUser()
        {
            var userId = int.Parse(_utilView.GetInput(Strings.UserIdInputMsg));

            _nhHelper.WithTransaction(session =>
            {
                var user = session.Get<User>(userId)
                    ?? throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));

                session.Delete(user);
            });

            _utilView.Output(string.Format(Strings.UserDeletedMsg, userId));
        }
    }
}
