using MiniBank.Views.Enums;
using System;

namespace MiniBank.Views
{
    public class UtilView
    {
        public ActionOption GetAction()
        {
            PrintMenu();
            var choosenAction = GetInput();

            if (int.TryParse(choosenAction, out int value) && Enum.IsDefined(typeof(ActionOption), value))
            {
                return (ActionOption)value;
            }

            Output(Strings.ChoiceOutOfRange);

            return GetAction();
        }

        private void PrintMenu()
        {
            Output(Strings.ActionInputMsg);

            foreach (var actionOption in Enum.GetValues(typeof(ActionOption)))
            {
                Output(string.Format(
                    Strings.MenuItemFormat,
                    (int)actionOption,
                    actionOption
                ));
            }
        }

        public string GetInput(string message = "")
        {
            if (message != "")
            {
                Output(message);
            }

            return Console.ReadLine();
        }

        public void Output(string message = "") => Console.WriteLine(message);

        public void Output(int message) => Console.WriteLine(message);
    }
}
