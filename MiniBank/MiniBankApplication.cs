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
        private NHibernateHelper _nhHelper;
        private UserController _userController;
        private AccountController _accountController;
        private readonly UtilView _utilView = new UtilView();

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

            _nhHelper = new NHibernateHelper(host, database, username, password);
        }

        private void InitiailzeControllers()
        {
            _accountController = new AccountController(_nhHelper);
            _userController = new UserController(_nhHelper);
        }

        public void Run()
        {
            var actions = GetActions();
            var choosenAction = _utilView.GetAction();

            while (choosenAction != ActionOption.Exit)
            {
                try
                {
                    actions[choosenAction]();
                }
                catch (Exception exception)
                {
                    _utilView.Output(string.Format(
                        Strings.ErrorMsg,
                        exception.Message,  
                        exception.StackTrace
                    ));
                }

                choosenAction = _utilView.GetAction();
            }
        }

        private Dictionary<ActionOption, Action> GetActions() =>
            new Dictionary<ActionOption, Action>
            {
                { ActionOption.GetUsers, _userController.GetAllUsers },
                { ActionOption.GetUserAccounts, _userController.GetUserAccounts },
                { ActionOption.Withdraw, _accountController.Withdraw },
                { ActionOption.Deposit, _accountController.Deposit },
                { ActionOption.CreateUser, _userController.CreateUser },
                { ActionOption.CreateAccount, _accountController.CreateAccount },
                { ActionOption.DeleteUser, _userController.DeleteUser },
                { ActionOption.DeleteAccount, _accountController.DeleteAccount },
                { ActionOption.Exit, () => _utilView.Output(Strings.ExitMsg) }
            };
    }
}
