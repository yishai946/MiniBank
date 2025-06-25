using MiniBank.Converters;
using MiniBank.DB;
using MiniBank.Models;
using MiniBank.Models.Enums;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MiniBank.Controllers
{
    public class UserController
    {
        public Database Database { get; set; }

        public UserController()
        {
            Database = new Database();
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = new MySqlCommand(Strings.GetUsersCommand, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32(Strings.IdVarName),
                            Name = reader.GetString(Strings.NameVarName)
                        });
                    }
                }
            }

            return users;
        }

        public List<Account> GetUserAccounts(int id)
        {
            var accounts = new List<Account>();

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = new MySqlCommand(
                    string.Format(Strings.GetUserAcoountCommand, id),
                    connection
                );

                using (var reader = command.ExecuteReader())
                {
                    var converter = new AccountConvertor();

                    while (reader.Read())
                    {
                        var accountId = reader.GetInt32(Strings.IdVarName);
                        var balance = reader.GetDecimal(Strings.BalanceVarName);
                        var type = reader.GetString(Strings.TypeVarName);

                        if (!Enum.TryParse<AccountType>(type, ignoreCase: true, out var accountType))
                        {
                            throw new InvalidOperationException(string.Format(Strings.UnsoppertedAccount, type));
                        }

                        accounts.Add(converter.Convert(accountType, accountId, balance));
                    }
                }
            }

            return accounts;
        }
    }
}
