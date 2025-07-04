using MiniBank.Views.Enums;
using System;
using System.Linq;

namespace MiniBank.Views
{
    public class ApplicationView
    {
        public ActionOption GetAction()
        {
            PrintMenu();
            var choosenAction = GetString();

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

            Enum.GetValues(typeof(ActionOption))
                .Cast<ActionOption>()
                .ToList()
                .ForEach(actionOption =>
                    Output(string.Format(
                        Strings.MenuItemFormat,
                        (int)actionOption,
                        actionOption
                    ))
                );
        }

        public string GetString(string message = "")
        {
            if (message != "")
            {
                Output(message);
            }

            return Console.ReadLine();
        }

        public int GetId(string idName)
        {
            string input = new ApplicationView().GetString(string.Format(Strings.IdInputMsg, idName));

            if (int.TryParse(input, out int number))
            {
                return number;
            }

            new ApplicationView().Output(Strings.InputError);

            return GetId(idName);
        }
        
        public decimal GetAmount()
        {
            string input = new ApplicationView().GetString(Strings.AmountInputMsg);

            if (decimal.TryParse(input, out decimal amount))
            {
                if (amount <= 0)
                {
                    new ApplicationView().Output(Strings.AmountErrorMsg);

                    return GetAmount();
                }

                return amount;
            }

            new ApplicationView().Output(Strings.InputError);

            return GetAmount();
        }

        public void Output(string message = "") => Console.WriteLine(message);

        public void Output(int message) => Console.WriteLine(message);
    }
}
