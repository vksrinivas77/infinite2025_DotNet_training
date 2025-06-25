using System;

namespace Assignment_3
{
    class Assignment_3
    {
        static void Main(string[] args)
        {
            Accounts accounts = new Accounts();
           
            Customer customer = new Customer(452345677, "Arun", "Saving", accounts.Balance);
            customer.Transaction_initiat(accounts);
            customer.ShowData();

            Customer customer2 = new Customer(2324345677, "Ajay", "Saving", accounts.Balance);
            customer2.Transaction_initiat(accounts);
            customer2.ShowData();
        }
    }

    class Accounts
    {
        protected double Account_no;
        protected string Customer_name;
        protected string Account_type;
        protected int balance=1000;

        public int Balance
        {
            get { return balance; }
        }

        public  void Credit(int amount)
        {
            this.balance += amount;
        }

        public void Debit(int amount)
        {
           this.balance -= amount;
        }
    }

    class Customer : Accounts
    {
        public Customer(double Account_no, string Customer_name, string Account_type, int balance)
        {
            this.Account_no = Account_no;
            this.Customer_name = Customer_name;
            this.Account_type = Account_type;
            this.balance = balance;
        }

        public void Transaction_initiat(Accounts accounts)
        {

            Console.WriteLine("Enter the Transaction_type for Deposit: 1, for Withdraw: 2");
            String Transaction_type = Convert.ToString(Console.ReadLine());
            if (Transaction_type == "1")
            {
                Console.WriteLine("Enter the Amount ");
                int amount = Convert.ToInt32(Console.ReadLine());
                accounts.Credit(amount);

            }
            else if (Transaction_type == "2")
            {
                Console.WriteLine("Enter the Amount");
                int amount = Convert.ToInt32(Console.ReadLine());
                accounts.Debit(amount);
            }
            else
            {
                Console.WriteLine("Enter valid Transaction_type");
            }
        
        }
        public void ShowData()
        {
            Console.WriteLine("The Customer Details");
            Console.WriteLine($"Customer name: {Customer_name}, Account no: {Account_no}, Account Type: {Account_type}, Balance: {balance}");
            Console.ReadLine();
        }

    }
}
