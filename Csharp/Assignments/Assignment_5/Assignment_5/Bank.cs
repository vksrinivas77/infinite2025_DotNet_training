using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5
{
class Bank
    {
        static void Main(string[] args)
        {

            BankAccount account = new BankAccount("Karan", 1000);

            account.Deposit(500);

            try
            {
                account.Withdraw(200);
                Console.ReadLine();
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            Console.WriteLine($"Current balance: {account.GetBalance()}");
            Console.ReadLine();

        }


    }

}

