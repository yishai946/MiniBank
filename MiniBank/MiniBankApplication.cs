using DotNetEnv;
using MiniBank.Controllers;
using MiniBank.DB;
using MiniBank.Views.Enums;
using MiniBank.Views;
using System;
using System.Collections.Generic;

namespace MiniBank
{
    public class MiniBankApplication
    {
        private readonly UserController UserController =
            new UserController(Database.Instance);
        private readonly AccountController AccountController =
            new AccountController(Database.Instance);

        public void Run()
        {
            var actions = GetActions();
            var choosenAction = new ApplicationView().GetAction();

            while (choosenAction != ActionOption.Exit)
            {
                try
                {
                    actions[choosenAction]();
                }
                catch (Exception exception)
                {
                    new ApplicationView().Output(string.Format(
                        Strings.ErrorMsg,
                        exception.Message,  
                        exception.StackTrace
                    ));
                }

                choosenAction = new ApplicationView().GetAction();
            }
        }

        private Dictionary<ActionOption, Action> GetActions() =>
            new Dictionary<ActionOption, Action>
            {
                { ActionOption.GetUsers, UserController.GetAllUsers },
                { ActionOption.GetUserAccounts, UserController.GetUserAccounts },
                { ActionOption.Withdraw, AccountController.Withdraw },
                { ActionOption.Deposit, AccountController.Deposit },
                { ActionOption.CreateUser, UserController.CreateUser },
                { ActionOption.CreateAccount, AccountController.CreateAccount },
                { ActionOption.DeleteUser, UserController.DeleteUser },
                { ActionOption.DeleteAccount, AccountController.DeleteAccount },
                { ActionOption.Exit, () => new ApplicationView().Output(Strings.ExitMsg) }
            };
    }
}
