using System;
namespace Assignment_5
{
    public class BankAccount
    {
        private string accountHolder;
        private double balance;

        public BankAccount(string accountHolder, double initialBalance)
        {
            this.accountHolder = accountHolder;
            this.balance = initialBalance;
        }

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine($"Deposited: {amount}");
            }
        }

        public void Withdraw(double amount)
        {
            if (amount > balance)
            {
                throw new InsufficientBalanceException("Insufficient balance for withdrawal!");
            }
            else
            {
                balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}");
            }
        }

        public double GetBalance()
        {
            return balance;
        }
    }
}