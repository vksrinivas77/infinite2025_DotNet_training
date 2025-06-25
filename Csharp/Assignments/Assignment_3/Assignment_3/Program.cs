using System;





namespace Assignment_3
{
    class Assignment_3
    {
        static void Main(string[] args) {
            Console.WriteLine("-------------bank-------------");
            Accounts accounts1 = new Accounts();
            Customer customer1 = new Customer(452345677, "Arun", "Saving");
            customer1.Transaction_initiat(accounts1);
            customer1.ShowData(accounts1);
            Console.WriteLine("----------student------------");
            Student stu = new Student(101, "Arjun", "B.Tech", 4, "CSE");
            stu.getMarks(new int[] { 45, 67, 88, 34, 56 });
            stu.displayData();
            stu.displayResult();
            Console.WriteLine("-------------sales-------------");
            Console.WriteLine("Enter sales No: ");
            int sales_No = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Product No: "); 
            int productNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Price: "); 
            double price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Quantity: "); 
            int qty = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Date of Sale: "); 
            string date = Console.ReadLine();
            Saledetails sale = new Saledetails(sales_No, productNo, price, qty, date);
            sale.showData(sale);
            Console.Read();




        }
    }
    class Accounts
    {
        protected double Account_no;
        protected string Customer_name;
        protected string Account_type;
        public int balance = 1000;
        public int Balance
        {
            get { return balance; }
        }
        public void Credit(int amount)
        {
            this.balance += amount;
        }
        public void Debit(int amount)
        {
            if (amount <= balance)
            {
                this.balance -= amount;
            }
            else
            {
                Console.WriteLine("Insufficient balance!");
            }
        }
    }
    class Customer
    {
        protected double Account_no;
        protected string Customer_name;
        protected string Account_type;

        public Customer(double Account_no, string Customer_name, string Account_type)
        {
            this.Account_no = Account_no;
            this.Customer_name = Customer_name;
            this.Account_type = Account_type;

        }
        public void Transaction_initiat(Accounts accounts)
        {
            Console.WriteLine("Enter the Transaction_type for Deposit: 1, for Withdraw: 2");
            string Transaction_type = Console.ReadLine();
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
        public void ShowData(Accounts accounts)
            {
                Console.WriteLine("The Customer Details");
                Console.WriteLine($"Customer name: {Customer_name}, Account no: {Account_no}, Account Type: {Account_type}, Balance: {accounts.Balance}");
                
            }
        }
       }
    
