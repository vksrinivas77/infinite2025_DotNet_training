using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//3.Create a class called Books with BookName and 
//    AuthorName as members. Instantiate the class through constructor 
//    and also write a method Display() to display the details. 

namespace Assignment_5
{
    class Book_main
    {
        static void Main(string[] args)
        {

            BookShelf shelf = new BookShelf();

            shelf[0] = new Book("1984", "George Orwell");
            shelf[1] = new Book("To Kill a Mockingbird", "Harper Lee");
            shelf[2] = new Book("The Hobbit", "J.R.R. Tolkien");
            shelf[3] = new Book("Pride and Prejudice", "Jane Austen");
            shelf[4] = new Book("Moby Dick", "Herman Melville");

            shelf.DisplayAll();
            Console.ReadLine();

        }
    }
}
