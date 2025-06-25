using MySql.Data.MySqlClient;

namespace MiniBank.Models
{
    public abstract class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public void Deposit(decimal amount) => Balance += amount;

        public abstract void Withdraw(decimal amount);

        public static Account CreateFromReader(MySqlDataReader reader)
        {
            var id = reader.GetInt32(Strings.IdVarName);
            var balance = reader.GetDecimal(Strings.BalanceVarName);
            var type = reader.GetString(Strings.TypeVarName);


        }
    }
}
