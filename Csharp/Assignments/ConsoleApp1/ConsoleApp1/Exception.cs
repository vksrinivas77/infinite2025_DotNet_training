using System;

namespace ConsoleApp1
{
    // Custom exception class
    
    class BankAccount
    {
        public string Name { get; set; }
        public int AccNo { get; set; }
        private decimal balance = 1000;

        public decimal Balance
        {
            get { return balance; }

            set { balance = value; }
        }

        public void Withdraw(decimal amount)
        {
            const decimal dailyLimit = 50000;

            if (amount > dailyLimit)
            {
                throw new DailyLimitExceededException("Withdrawal amount exceeds the daily limit.");
            }
            else if (amount <= Balance)
            {
                Balance -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient balance.");
            }
        }
    }

    class Progr
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount { Name = "Alice", AccNo = 12345 };

            try
            {
                account.Withdraw(60000); // This will throw DailyLimitExceededException
            }
            catch (DailyLimitExceededException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
