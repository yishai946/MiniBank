using System.Collections.Generic;
using MiniBank.Models;

namespace MiniBank.Views
{
    public class UserView
    {
        public void ShowUsers(List<User> users)
        {
            new ApplicationView().Output(Strings.UsersViewHeader);

            users.ForEach(user => ShowUser(user));
        }

        public void ShowUser(User user) =>
            new ApplicationView().Output(string.Format(Strings.UserFormat, user.Id, user.Name));
    }
}
