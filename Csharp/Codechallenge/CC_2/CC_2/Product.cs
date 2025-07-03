using System;
using System.Collections.Generic;
using System.Linq;


public class Product
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }

   
    public Product(string productId, string productName, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
    }
    public override string ToString()
    {
        return $"Product(ID: {ProductId}, Name: {ProductName}, Price: {Price:F2})";
    }
}

public class Product_Sort
{
    public static void Main(string[] args)
    {
        int numberOfProductsToAccept = 3;
        List<Product> allProducts = new List<Product>();

        Console.WriteLine($"Please enter details for {numberOfProductsToAccept} products:");

        //  Accept 10 Products 
        for (int i = 0; i < numberOfProductsToAccept; i++)
        {
            Console.WriteLine($"--- Product {i + 1} ---");
            Console.Write("Enter Product ID: ");
            string productId = Console.ReadLine();

            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Enter Price: ");
            int price = Convert.ToInt32(Console.ReadLine());
           
            allProducts.Add(new Product(productId, productName, price));
        }
        List<Product> sortedProducts = allProducts.OrderBy(p => p.Price).ToList();
        Console.WriteLine("-- Products Sorted by Price  ---");
        foreach (var product in sortedProducts)
        {
            Console.WriteLine(product); 
        }
        Console.ReadLine();
    }
}
