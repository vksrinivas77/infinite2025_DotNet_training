using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    class Saledetails
    {
        int salesNo;
        int productNo;
        double price;
        String dateOfSale;
        int qty;
        double totalAmount;
        public Saledetails(int salesNo, int productNo, double price, int qty, String dateOfSale)
        {
            this.salesNo = salesNo;
            this.productNo = productNo;
            this.price = price;
            this.qty = qty;
            this.dateOfSale = dateOfSale;
            sales();
           }
            public void sales() {
                totalAmount = price * qty;
            }
            public void showData(Saledetails obj)
            {
                Console.WriteLine("---Sales Details-- - ");

                Console.WriteLine("Sales No: " + obj.salesNo);
                Console.WriteLine("the Product No: " + obj.productNo);
                Console.WriteLine("Price: " + obj.price);
                Console.WriteLine("Quantity: " + obj.qty);
                Console.WriteLine("Date of Sale: " + obj.dateOfSale);
                Console.WriteLine("Total Amount: " + obj.totalAmount); 
             }
      }
  }
    


