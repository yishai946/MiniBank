using MiniBank.DB;
using MiniBank.Models;
using MiniBank.Views;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniBank.Controllers
{
    public class UserController
    {
        private readonly NHibernateHelper NhHelper;

        public UserController(NHibernateHelper nhHelper)
        {
            NhHelper = nhHelper;
        }

        public void GetAllUsers()
        {
            var users = NhHelper.WithSession(session => session.Query<User>().ToList());

            new UserView().ShowUsers(users);
        }

        public void GetUserAccounts() {
            var userId = new ApplicationView().GetId(Strings.UserVarName);

            var accounts = 
                NhHelper.WithSession(session =>
                {
                    var user = session.Get<User>(userId);

                    if (user == null) {
                        throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));
                    }

                    return user.Accounts.ToList();
                });

            new AccountView().ShowAccounts(accounts);
        }

        public void CreateUser()
        {
            var name = new ApplicationView().GetString(Strings.NameInputMsg);

            var newUserId = 
                NhHelper.WithTransaction(session =>
                {
                    var newUser = new User { Name = name };
                    session.Save(newUser);

                    return newUser.Id;
                }
            );

            new ApplicationView().Output(string.Format(Strings.UserCreatedMsg, newUserId));
        }

        public void DeleteUser()
        {
            var userId = new ApplicationView().GetId(Strings.UserVarName);

            NhHelper.WithTransaction(session =>
            {
                var userToDelete = session.Get<User>(userId);

                if (userToDelete == null) {
                    throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));
                }

                var accountsToCheck = userToDelete.Accounts.ToList();

                accountsToCheck.ForEach(account =>
                {
                    if (account.Users.Count == 1)
                    {
                        session.Delete(account);
                    }
                });

                session.Delete(userToDelete);
            });

            new ApplicationView().Output(string.Format(Strings.UserDeletedMsg, userId));
        }
    }
}
