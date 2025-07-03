using MiniBank.DB;
using MiniBank.Models;
using MiniBank.Views;
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
            var userId = int.Parse(new UtilView().GetInput(Strings.UserIdInputMsg));

            var accounts = 
                NhHelper.WithSession(session =>
                {
                    var user = session.Get<User>(userId)
                        ?? throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));

                    return user.Accounts.ToList();
                });

            new AccountView().ShowAccounts(accounts);
        }

        public void CreateUser()
        {
            var name = new UtilView().GetInput(Strings.NameInputMsg);

            var newUserId = 
                NhHelper.WithTransaction(session =>
                {
                    var newUser = new User { Name = name };
                    session.Save(newUser);

                    return newUser.Id;
                }
            );

            new UtilView().Output(string.Format(Strings.UserCreatedMsg, newUserId));
        }

        public void DeleteUser()
        {
            var userId = int.Parse(new UtilView().GetInput(Strings.UserIdInputMsg));

            NhHelper.WithTransaction(session =>
            {
                var user = session.Get<User>(userId)
                    ?? throw new KeyNotFoundException(string.Format(Strings.UserNotFound, userId));

                session.Delete(user);
            });

            new UtilView().Output(string.Format(Strings.UserDeletedMsg, userId));
        }
    }
}
