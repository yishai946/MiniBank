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
        private NHibernateHelper NhHelper;
        private UserController UserController;
        private AccountController AccountController;

        public MiniBankApplication()
        {
            Env.Load();

            InitializeDB();
            InitiailzeControllers();
        }

        private void InitializeDB()
        {
            var host = Environment.GetEnvironmentVariable(Strings.HostVarName);
            var database = Environment.GetEnvironmentVariable(Strings.DBVarName);
            var username = Environment.GetEnvironmentVariable(Strings.UsernameVarName);
            var password = Environment.GetEnvironmentVariable(Strings.PasswordVarName);

            NhHelper = new NHibernateHelper(host, database, username, password);
        }

        private void InitiailzeControllers()
        {
            AccountController = new AccountController(NhHelper);
            UserController = new UserController(NhHelper);
        }

        public void Run()
        {
            var actions = GetActions();
            var choosenAction = new UtilView().GetAction();

            while (choosenAction != ActionOption.Exit)
            {
                try
                {
                    actions[choosenAction]();
                }
                catch (Exception exception)
                {
                    new UtilView().Output(string.Format(
                        Strings.ErrorMsg,
                        exception.Message,  
                        exception.StackTrace
                    ));
                }

                choosenAction = new UtilView().GetAction();
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
                { ActionOption.Exit, () => new UtilView().Output(Strings.ExitMsg) }
            };
    }
}
