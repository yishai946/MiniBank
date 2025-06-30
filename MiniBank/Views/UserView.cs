using MiniBank.Models;
using System;
using System.Collections.Generic;

namespace MiniBank.Views
{
    public class UserView
    {
        private readonly UtilView _utilView = new UtilView();

        public void ShowUsers(List<User> users)
        {
            _utilView.Output(Strings.UsersViewHeader);

            users.ForEach(user => ShowUser(user));
        }

        public void ShowUser(User user) =>
            _utilView.Output(string.Format(Strings.UserFormat, user.Id, user.Name));
    }
}
